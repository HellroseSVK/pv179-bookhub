using BusinessLayer.DTOs.Review;

namespace BusinessLayer.Services.Interfaces;

public interface IReviewService
{
    Task<ReviewDTO> CreateReviewAsync(ReviewUpsertDTO review);
    Task<List<ReviewDTO>> GetReviewsAsync(
        int[]? ids = null,
        bool includeBook = true,
        bool includeUser = true);
    Task<ReviewDTO> GetReviewAsync(
        int id,
        bool includeBook = true,
        bool includeUser = true);
    Task PatchReviewAsync(int id, ReviewPartialDTO review);
    Task UpdateReviewAsync(int id, ReviewUpsertDTO review);
    Task DeleteReviewAsync(int id);
}