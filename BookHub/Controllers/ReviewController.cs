using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Core;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.Response;
using WebAPI.Models.Response.Simplified;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{
    private readonly CustomDBContext _dbContext;

    public ReviewController(CustomDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    [HttpGet]
    [ProducesResponseType<List<ReviewResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReviews()
    {
        var reviews = await _dbContext.Reviews
            .Include(review => review.Book)
            .Include(review => review.User)
            .ToListAsync();

        return Ok(reviews.Select(r => r.Adapt<ReviewResponse>() with
        {
            Book = r.Book.Adapt<BookSimplifiedResponse>(),
            User = r.User.Adapt<UserSimplifiedResponse>()
        }).ToList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType<ReviewResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReview(int id)
    {
        var review = await (FindReview(id));

        return Ok(review.Adapt<ReviewResponse>() with
        {
            Book = review.Book.Adapt<BookSimplifiedResponse>(),
            User = review.User.Adapt<UserSimplifiedResponse>()
        });
    }

    [HttpPost]
    [ProducesResponseType<ReviewResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateReview([FromBody] ReviewModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var review = new Review
        {
            Rating = model.Rating,
            Body = model.Body,
            BookId = model.BookId,
            UserId = model.UserId
        };

        _dbContext.Reviews.Add(review);

        await _dbContext.SaveChangesAsync();

        return Created($"/Review/{review.Id}", review.Adapt<ReviewResponse>() with
        {
            Book = review.Book.Adapt<BookSimplifiedResponse>(),
            User = review.User.Adapt<UserSimplifiedResponse>()
        });
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchReview(int id, [FromBody] ReviewPartialModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var review = await FindReview(id);

        review.Rating = model.Rating ?? review.Rating;
        review.Body = model.Body ?? review.Body;
        review.BookId = model.BookId ?? review.BookId;
        review.UserId = model.UserId ?? review.UserId;

        _dbContext.Reviews.Update(review);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var review = await FindReview(id);

        review.Rating = model.Rating;
        review.Body = model.Body;
        review.BookId = model.BookId;
        review.UserId = model.UserId;

        _dbContext.Reviews.Update(review);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteReview(int id)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var review = await FindReview(id);

        _dbContext.Reviews.Remove(review);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private async Task<Review> FindReview(int reviewId)
    {
        var review = await _dbContext.Reviews
            .FirstOrDefaultAsync(p => p.Id == reviewId);

        if (review == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Review with id {reviewId} not found.");
        }

        return review;
    }
}