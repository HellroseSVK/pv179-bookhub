using BusinessLayer.DTOs.Review;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebAPI.Exception;

namespace BusinessLayer.Services;

public class ReviewService(CustomDBContext dbContext) : IReviewService
{
    public async Task<ReviewSimplifiedDTO> CreateReviewAsync(ReviewSimplifiedDTO reviewDto)
    {
        var created = dbContext.Reviews.Add(reviewDto.Adapt<Review>());
        await dbContext.SaveChangesAsync();

        return created.Entity.Adapt<ReviewSimplifiedDTO>();
    }

    public async Task<ReviewSimplifiedDTO> GetReviewAsync(int id, bool includeBook = true, bool includeUser = true)
    {
        var review = await FindReview(id, includeBook, includeUser);
        return review.Adapt<ReviewSimplifiedDTO>();
    }

    public async Task<List<ReviewSimplifiedDTO>> GetReviewsAsync(int[]? ids = null, bool includeBook = true, bool includeUser = true)
    {
        var query = GetBaseQuery(includeBook, includeUser);

        if (ids is { Length: > 0 })
        {
            query = query.Where(r => ids.Contains(r.Id));
        }

        var reviews = await query.ToListAsync();
        return reviews.Adapt<List<ReviewSimplifiedDTO>>();
    }

    public async Task PatchReviewAsync(int id, ReviewPartialDTO reviewDto)
    {
        var review = await FindReview(id);

        review.Rating = reviewDto.Rating ?? review.Rating;
        review.Body = reviewDto.Body ?? review.Body;
        review.BookId = reviewDto.BookId ?? review.BookId;
        review.UserId = reviewDto.UserId ?? review.UserId;

        dbContext.Reviews.Update(review);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateReviewAsync(int id, ReviewSimplifiedDTO reviewDto)
    {
        var review = await FindReview(id);

        review.Rating = reviewDto.Rating;
        review.Body = reviewDto.Body;
        review.BookId = reviewDto.BookId;
        review.UserId = reviewDto.UserId;

        dbContext.Reviews.Update(review);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteReviewAsync(int id)
    {
        var review = await FindReview(id);
        
        dbContext.Reviews.Remove(review);
        await dbContext.SaveChangesAsync();
    }

    private async Task<Review> FindReview(int reviewId, bool includeBook = false, bool includeUser = false)
    {
        var query = GetBaseQuery(includeBook, includeUser);
        var review = await query.FirstOrDefaultAsync(r => r.Id == reviewId);

        return review ??
               throw new StatusCodeException(StatusCodes.Status404NotFound, $"Review with id {reviewId} not found.");
    }

    private IQueryable<Review> GetBaseQuery(bool includeBook, bool includeUser)
    {
        IQueryable<Review> query = dbContext.Reviews;

        if (includeBook)
        {
            query = query.Include(r => r.Book);
        }

        if (includeUser)
        {
            query = query.Include(r => r.User);
        }

        return query;
    }
}