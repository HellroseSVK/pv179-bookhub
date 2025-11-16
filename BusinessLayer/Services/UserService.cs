using BusinessLayer.DTOs.User;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services;

public class UserService(CustomDBContext dbContext) : IUserService
{
    public async Task<UserDTO> CreateUserAsync(UserUpsertDTO userDto)
    {
        var created = dbContext.Users.Add(userDto.Adapt<User>());

        await dbContext.SaveChangesAsync();

        return created.Entity.Adapt<UserDTO>();
    }

    public async Task<UserDTO> GetUserAsync(int id, bool includeReviews = true,
        bool includeWishlistItems = true, bool includeOrderItems = true)
    {
        var user = await FindUser(id, includeReviews, includeWishlistItems, includeOrderItems);

        return user.Adapt<UserDTO>();
    }

    public async Task<List<UserDTO>> GetUsersAsync(int[]? ids = null, bool includeReviews = true,
        bool includeWishlistItems = true, bool includeOrderItems = true)
    {
        var query = GetBaseQuery(includeReviews, includeWishlistItems, includeOrderItems);

        if (ids is { Length: > 0 })
        {
            query = query.Where(a => ids.Contains(a.Id));
        }

        var users = await query.ToListAsync();

        return users.Adapt<List<UserDTO>>();
    }

    public async Task PatchUserAsync(int id, UserPartialDTO userDto)
    {
        var loaded = await FindUser(id);

        loaded.Email = userDto.Email ?? loaded.Email;
        loaded.Password = userDto.Password ?? loaded.Password;
        loaded.IsAdmin = userDto.IsAdmin ?? loaded.IsAdmin;

        dbContext.Users.Update(loaded);

        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(int id, UserUpsertDTO userDto)
    {
        var loaded = await FindUser(id);

        loaded.Email = userDto.Email;
        loaded.Password = userDto.Password;
        loaded.IsAdmin = userDto.IsAdmin;

        dbContext.Users.Update(loaded);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await FindUser(id);

        dbContext.Users.Remove(user);

        await dbContext.SaveChangesAsync();
    }

    private async Task<User> FindUser(int userId, bool includeReviews = false, bool includeWishlistItems = false,
        bool includeOrderItems = false)
    {
        var query = GetBaseQuery(includeReviews, includeWishlistItems, includeOrderItems);

        var user = await query.FirstOrDefaultAsync(user => user.Id == userId);

        return user ??
               throw new StatusCodeException(StatusCodes.Status404NotFound, $"User with id {userId} not found.");
    }

    private IQueryable<User> GetBaseQuery(bool includeReviews, bool includeWishlistItems, bool includeOrderItems)
    {
        IQueryable<User> userQuery = dbContext.Users;

        if (includeReviews)
        {
            userQuery = userQuery
                .Include(user => user.Reviews);
        }

        if (includeWishlistItems)
        {
            userQuery = userQuery
                .Include(user => user.WishlistItems);
        }

        if (includeOrderItems)
        {
            userQuery = userQuery
                .Include(user => user.OrderItems);
        }

        return userQuery;
    }
}