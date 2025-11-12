using BusinessLayer.DTOs.OrderItem;
using BusinessLayer.DTOs.WishlistItem;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services
{
    public class WishlistItemService(CustomDBContext dbContext) : IWishlistItemService
    {
        public async Task<WishlistItemDTO> CreateWishlistItemAsync(WishlistItemUpsertDTO wishlistItemDto)
        {
            var book = await FindBook(wishlistItemDto.BookId);
            var user = await FindUser(wishlistItemDto.UserId);

            var wishlistItem = wishlistItemDto.Adapt<WishlistItem>();
            wishlistItem.BookId = book.Id;
            wishlistItem.Book = book;
            wishlistItem.UserId = user.Id;
            wishlistItem.User = user;

            var created = dbContext.WishlistItems.Add(wishlistItem);
            await dbContext.SaveChangesAsync();

            return created.Entity.Adapt<WishlistItemDTO>();

        }

        public async Task<WishlistItemDTO> GetWishlistItemAsync(int id, bool includeBook = true, bool includeUser = true)
        {
            var item = await FindWishlistItem(id, includeBook, includeUser);

            return item.Adapt<WishlistItemDTO>();
        }

        public async Task<List<WishlistItemDTO>> GetWishlistItemsAsync(int[]? ids = null, bool includeBook = true, bool includeUser = true)
        {
            var query = GetBaseQuery(includeBook, includeUser);

            if (ids is { Length: > 0 })
            {
                query = query.Where(item => ids.Contains(item.Id));
            }

            var wishlistItems = await query.ToListAsync();
            return wishlistItems.Adapt<List<WishlistItemDTO>>();

        }

        public async Task PatchWishlistItemAsync(int id, WishlistItemPartialDTO wishlistItemDto)
        {
            var wishlistItem = await FindWishlistItem(id);

            if (wishlistItemDto.BookId != null)
            {
                var book = await FindBook((int)wishlistItemDto.BookId);
                wishlistItem.BookId = book.Id;
                wishlistItem.Book = book;
            }

            if (wishlistItemDto.UserId != null)
            {
                var user = await FindUser((int)wishlistItemDto.UserId);
                wishlistItem.UserId = user.Id;
                wishlistItem.User = user;
            }

            dbContext.WishlistItems.Update(wishlistItem);
            await dbContext.SaveChangesAsync();

        }
        public async Task UpdateWishlistItemAsync(int id, WishlistItemUpsertDTO wishlistItemDto)
        {
            var wishlistItem = await FindWishlistItem(id);

            var book = await FindBook(wishlistItemDto.BookId);
            var user = await FindUser(wishlistItemDto.UserId);

            wishlistItem.BookId = book.Id;
            wishlistItem.Book = book;
            wishlistItem.UserId = user.Id;
            wishlistItem.User = user;

            dbContext.WishlistItems.Update(wishlistItem);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteWishlistItemAsync(int id)
        {
            var item = await FindWishlistItem(id);

            dbContext.WishlistItems.Remove(item);

            await dbContext.SaveChangesAsync();
        }

        private async Task<WishlistItem> FindWishlistItem(int wishlistItemId, bool includeBook = false, bool includeUser = false)
        {
            var query = GetBaseQuery(includeBook, includeUser);
            var wishlistItem = await query.FirstOrDefaultAsync(item => item.Id == wishlistItemId);

            return wishlistItem ?? throw new StatusCodeException(StatusCodes.Status404NotFound, $"OrderItem with id {wishlistItemId} not found.");

        }

        private async Task<Book> FindBook(int bookId)
        {
            var book = await dbContext.Books
                .FirstOrDefaultAsync(book => bookId == book.Id);

            if (book == null)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound, $"Book with id {string.Join(", ", bookId)} not found.");
            }

            return book;
        }

        private async Task<User> FindUser(int userId)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(user => userId == user.Id);

            if (user == null)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound, $"User with id {string.Join(", ", userId)} not found.");
            }

            return user;
        }

        private IQueryable<WishlistItem> GetBaseQuery(bool includeBook, bool includeUser)
        {
            IQueryable<WishlistItem> query = dbContext.WishlistItems;

            if (includeBook)
            {
                query = query.Include(item => item.Book);
            }

            if (includeUser)
            {
                query = query.Include(item => item.User);
            }

            return query;
        }
    }
}
