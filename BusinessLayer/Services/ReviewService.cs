using BusinessLayer.DTOs.Review;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services;

public class ReviewService(CustomDBContext dbContext) : IReviewService
{
    public async Task<ReviewDTO> CreateReviewAsync(ReviewUpsertDTO reviewDto)
    {
        var book = await FindBook(reviewDto.BookId);
        var user = await FindUser(reviewDto.UserId);

        var review = reviewDto.Adapt<Review>();
        review.BookId = book.Id;
        review.Book = book;
        review.UserId = user.Id;
        review.User = user;

        var created = dbContext.Reviews.Add(reviewDto.Adapt<Review>());
        await dbContext.SaveChangesAsync();

        return created.Entity.Adapt<ReviewDTO>();
    }

    public async Task<ReviewDTO> GetReviewAsync(int id, bool includeBook = true, bool includeUser = true)
    {
        var review = await FindReview(id, includeBook, includeUser);
        return review.Adapt<ReviewDTO>();
    }

    public async Task<List<ReviewDTO>> GetReviewsAsync(int[]? ids = null, bool includeBook = true, bool includeUser = true)
    {
        var query = GetBaseQuery(includeBook, includeUser);

        if (ids is { Length: > 0 })
        {
            query = query.Where(r => ids.Contains(r.Id));
        }

        var reviews = await query.ToListAsync();
        return reviews.Adapt<List<ReviewDTO>>();
    }

    public async Task PatchReviewAsync(int id, ReviewPartialDTO reviewDto)
    {
        var review = await FindReview(id);

        if (reviewDto.BookId != null)
        {
            var book = await FindBook((int)reviewDto.BookId);
            review.BookId = book.Id;
            review.Book = book;
        }

        if (reviewDto.UserId != null)
        {
            var user = await FindUser((int)reviewDto.UserId);
            review.UserId = user.Id;
            review.User = user;
        }

        dbContext.Reviews.Update(review);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateReviewAsync(int id, ReviewUpsertDTO reviewDto)
    {
        var review = await FindReview(id);

        var book = await FindBook(reviewDto.BookId);
        var user = await FindUser(reviewDto.UserId);

        review.BookId = book.Id;
        review.Book = book;
        review.UserId = user.Id;
        review.User = user;

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

    private async Task<Book> FindBook(int userId)
    {
        var book = await dbContext.Books
            .Where(book => userId.Equals(book.Id))
            .FirstAsync();

        if (book == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Book with id {string.Join(", ", userId)} not found.");
        }

        return book;
    }

    private async Task<User> FindUser(int userId)
    {
        var user = await dbContext.Users
            .Where(user => userId.Equals(user.Id))
            .FirstAsync();

        if (user == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"User with id {string.Join(", ", userId)} not found.");
        }

        return user;
    }
}