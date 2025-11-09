using BusinessLayer.DTOs.Review;

namespace BusinessLayer.Services.Interfaces;

public interface IReviewService
{
    Task<ReviewSimplifiedDTO> CreateReviewAsync(ReviewSimplifiedDTO review);
    Task<List<ReviewSimplifiedDTO>> GetReviewsAsync(
        int[]? ids = null,
        bool includeBook = true,
        bool includeUser = true);
    Task<ReviewSimplifiedDTO> GetReviewAsync(
        int id,
        bool includeBook = true,
        bool includeUser = true);
    Task PatchReviewAsync(int id, ReviewPartialDTO review);
    Task UpdateReviewAsync(int id, ReviewSimplifiedDTO review);
    Task DeleteReviewAsync(int id);
}