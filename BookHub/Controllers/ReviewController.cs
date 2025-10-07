using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Core;
using WebAPI.Exception;
using WebAPI.Models;

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
    [ProducesResponseType<List<Review>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReviews()
    {
        var reviews = await _dbContext.Reviews.ToListAsync();

        return Ok(reviews);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<Review>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReview(int id)
    {
        var review = await (FindReview(id));

        return Ok(review);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
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

        return Created();
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