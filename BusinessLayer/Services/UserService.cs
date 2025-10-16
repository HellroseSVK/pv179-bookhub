using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Simplified;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly CustomDBContext _dbContext;

        public UserService(CustomDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<UserSimplifiedDTO> CreateUserAsync(UserSimplifiedDTO user)
        {
            var created = _dbContext.Users.Add(user.Adapt<User>());

            await _dbContext.SaveChangesAsync();

            return created.Adapt<UserSimplifiedDTO>();
        }

        public async Task<UserSimplifiedDTO?> GetUserAsync(int id, bool includeReviews = true, bool includeWishlistItems = true, bool includeOrderItems = true)
        {
            var query = GetBaseQuery(includeReviews, includeWishlistItems, includeOrderItems);

            var user = await query.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                return null;
            }

            return user.Adapt<UserSimplifiedDTO>();

        }

        public async Task<List<UserSimplifiedDTO>> GetUsersAsync(int[]? ids = null, bool includeReviews = true, bool includeWishlistItems = true, bool includeOrderItems = true)
        {
            var query = GetBaseQuery(includeReviews, includeWishlistItems, includeOrderItems);

            if (ids != null && ids.Length > 0)
            {
                query = query.Where(a => ids.Contains(a.Id));
            }

            var users = await query.ToListAsync();

            if (users == null)
            {
                return [];
            }

            return users.Adapt<List<UserSimplifiedDTO>>();
        }

        public async Task<bool> PatchUserAsync(int id, UserSimplifiedPartialDTO user)
        {
            var query = GetBaseQuery(false, false, false);
            var loaded = await query.FirstOrDefaultAsync(user => user.Id == id);

            if (loaded == null)
            {
                return false;
            }

            loaded.Email = user.Email ?? loaded.Email;
            loaded.Password = user.Password ?? loaded.Password;
            loaded.IsAdmin = user.IsAdmin ?? loaded.IsAdmin;

            _dbContext.Users.Update(loaded);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserAsync(int id, UserSimplifiedDTO user)
        {
            var query = GetBaseQuery(false, false, false);
            var loaded = await query.FirstOrDefaultAsync(user => user.Id == id);

            if (loaded == null)
            {
                return false;
            }

            loaded.Email = user.Email;
            loaded.Password = user.Password;
            loaded.IsAdmin = user.IsAdmin;

            _dbContext.Users.Update(loaded);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                return false;
            }

            _dbContext.Users.Remove(user);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        private IQueryable<User> GetBaseQuery(bool includeReviews, bool includeWishlistItems, bool includeOrderItems)
        {
            IQueryable<User> userQuery = _dbContext.Users;

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
