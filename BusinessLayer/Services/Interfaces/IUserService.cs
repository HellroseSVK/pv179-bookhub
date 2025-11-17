using BusinessLayer.DTOs.User;

namespace BusinessLayer.Services.Interfaces;

public interface IUserService
{
    public Task<UserDTO> CreateUserAsync(UserUpsertDTO userDto);

    public Task<List<UserDTO>> GetUsersAsync(
        int[]? ids = null,
        bool includeReviews = true,
        bool includeWishlistItems = true,
        bool includeOrderItems = true);

    public Task<UserDTO> GetUserAsync(
        int id,
        bool includeReviews = true,
        bool includeWishlistItems = true,
        bool includeOrderItems = true);

    public Task PatchUserAsync(int id, UserPartialDTO userDto);
    public Task UpdateUserAsync(int id, UserUpsertDTO userDto);
    public Task DeleteUserAsync(int id);
}