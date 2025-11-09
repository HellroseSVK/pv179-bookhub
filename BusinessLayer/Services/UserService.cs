using BusinessLayer.DTOs.User;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebAPI.Exception;

namespace BusinessLayer.Services
{
    public class UserService(CustomDBContext dBContext) : IUserService
    {
        public async Task<UserSimplifiedDTO> CreateUserAsync(UserSimplifiedDTO user)
        {
            var created = dBContext.Users.Add(user.Adapt<User>());

            await dBContext.SaveChangesAsync();

            return created.Entity.Adapt<UserSimplifiedDTO>();
        }

        public async Task<UserSimplifiedDTO> GetUserAsync(int id, bool includeReviews = true,
            bool includeWishlistItems = true, bool includeOrderItems = true)
        {
            var user = await FindUser(id, includeReviews, includeWishlistItems, includeOrderItems);

            return user.Adapt<UserSimplifiedDTO>();
        }

        public async Task<List<UserSimplifiedDTO>> GetUsersAsync(int[]? ids = null, bool includeReviews = true,
            bool includeWishlistItems = true, bool includeOrderItems = true)
        {
            var query = GetBaseQuery(includeReviews, includeWishlistItems, includeOrderItems);

            if (ids is { Length: > 0 })
            {
                query = query.Where(a => ids.Contains(a.Id));
            }

            var users = await query.ToListAsync();

            return users.Adapt<List<UserSimplifiedDTO>>();
        }

        public async Task PatchUserAsync(int id, UserSimplifiedPartialDTO user)
        {
            var loaded = await FindUser(id);

            loaded.Email = user.Email ?? loaded.Email;
            loaded.Password = user.Password ?? loaded.Password;
            loaded.IsAdmin = user.IsAdmin ?? loaded.IsAdmin;

            dBContext.Users.Update(loaded);

            await dBContext.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int id, UserSimplifiedDTO user)
        {
            var loaded = await FindUser(id);

            loaded.Email = user.Email;
            loaded.Password = user.Password;
            loaded.IsAdmin = user.IsAdmin;

            dBContext.Users.Update(loaded);

            await dBContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await FindUser(id);

            dBContext.Users.Remove(user);

            await dBContext.SaveChangesAsync();
        }

        private async Task<User> FindUser(int userId, bool includeReviews = false, bool includeWishlistItems = false, bool includeOrderItems = false)
        {
            var query = GetBaseQuery(includeReviews, includeWishlistItems, includeOrderItems);

            var user = await query.FirstOrDefaultAsync(user => user.Id == userId);

            return user ??
                   throw new StatusCodeException(StatusCodes.Status404NotFound, $"User with id {userId} not found.");
        }

        private IQueryable<User> GetBaseQuery(bool includeReviews, bool includeWishlistItems, bool includeOrderItems)
        {
            IQueryable<User> userQuery = dBContext.Users;

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
}