using BusinessLayer.DTOs.User;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserSimplifiedDTO> CreateUserAsync(UserSimplifiedDTO user);

        public Task<List<UserSimplifiedDTO>> GetUsersAsync(
            int[]? ids = null,
            bool includeReviews = true,
            bool includeWishlistItems = true,
            bool includeOrderItems = true);

        public Task<UserSimplifiedDTO> GetUserAsync(
            int id,
            bool includeReviews = true,
            bool includeWishlistItems = true,
            bool includeOrderItems = true);

        public Task PatchUserAsync(int id, UserSimplifiedPartialDTO user);
        public Task UpdateUserAsync(int id, UserSimplifiedDTO user);
        public Task DeleteUserAsync(int id);
    }
}