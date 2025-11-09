using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;
using WebAPI.Core;
using WebAPI.Models.Book;
using WebAPI.Models.Review;
using WebAPI.Models.User;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController(CustomDBContext dBContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<List<ReviewResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetReviews()
    {
        var reviews = await dBContext.Reviews
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
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
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
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
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

        dBContext.Reviews.Add(review);

        await dBContext.SaveChangesAsync();

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
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
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

        dBContext.Reviews.Update(review);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
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

        dBContext.Reviews.Update(review);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteReview(int id)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var review = await FindReview(id);

        dBContext.Reviews.Remove(review);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }

    private async Task<Review> FindReview(int reviewId)
    {
        var review = await dBContext.Reviews
            .FirstOrDefaultAsync(p => p.Id == reviewId);

        if (review == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Review with id {reviewId} not found.");
        }

        return review;
    }
}