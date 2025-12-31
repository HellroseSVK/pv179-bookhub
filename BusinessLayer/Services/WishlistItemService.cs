using BusinessLayer.DTOs;
using BusinessLayer.DTOs.WishlistItem;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services;

public class WishlistItemService(CustomDBContext dbContext) : IWishlistItemService
{
    public async Task<WishlistItemDTO> CreateWishlistItemAsync(WishlistItemUpsertDTO wishlistItemDto)
    {
        var book = await FindBook(wishlistItemDto.BookId);
        var user = await FindUser(wishlistItemDto.UserId);

        // Prevent duplicates: if user already has this book in wishlist, throw
        var exists = await dbContext.WishlistItems
            .AnyAsync(w => w.BookId == wishlistItemDto.BookId && w.UserId == wishlistItemDto.UserId);
        if (exists)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, "Book is already in wishlist.");
        }

        var wishlistItem = wishlistItemDto.Adapt<WishlistItem>();
        wishlistItem.BookId = book.Id;
        wishlistItem.Book = book;
        wishlistItem.UserId = user.Id;
        wishlistItem.User = user;


        var created = dbContext.WishlistItems.Add(wishlistItem);
        await dbContext.SaveChangesAsync();

        return created.Entity.Adapt<WishlistItemDTO>();

    }

    public async Task<PagedResult<WishlistItemDTO>> GetWishlistItemsPaginatedAsync(PaginationParameters paginationParameters, int? userId, bool includeBook = true,
        bool includeUser = true)
    {
        var query = GetBaseQuery(includeBook, includeUser);

        if (userId != null)
        {
            query = query.Where(item => item.UserId == userId);
        }

        var totalCount = await query.CountAsync();

        var wishlistItems = await query.ToListAsync();

        return new PagedResult<WishlistItemDTO>
        {
            Items = wishlistItems.Adapt<List<WishlistItemDTO>>(),
            TotalCount = totalCount,
            PageNumber = paginationParameters.PageNumber,
            PageSize = paginationParameters.PageSize
        };
    }

    public async Task<WishlistItemDTO> GetWishlistItemAsync(int id, bool includeBook = true, bool includeUser = true)
    {
        var item = await FindWishlistItem(id, includeBook, includeUser);

        return item.Adapt<WishlistItemDTO>();
    }

    public async Task<List<WishlistItemDTO>> GetWishlistItemsAsync(int? userId = null, bool includeBook = true, bool includeUser = true)
    {
        var query = GetBaseQuery(includeBook, includeUser);

        if (userId != null)
        {
            query = query.Where(item => item.UserId == userId);
        }

        var wishlistItems = await query.ToListAsync();
        return wishlistItems.Adapt<List<WishlistItemDTO>>();
    }

    public async Task PatchWishlistItemAsync(int id, WishlistItemPartialDTO wishlistItemDto)
    {
        var wishlistItem = await FindWishlistItem(id);

        if (wishlistItemDto.BookId != null)
        {
            var book = await FindBook(wishlistItemDto.BookId.Value);
            wishlistItem.BookId = book.Id;
            wishlistItem.Book = book;
        }

        if (wishlistItemDto.UserId != null)
        {
            var user = await FindUser(wishlistItemDto.UserId.Value);
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

    private async Task<Book> FindBook(int bookId)
    {
        var book = await dbContext.Books
            .FirstOrDefaultAsync(book => bookId.Equals(book.Id));

        if (book == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Book with id {string.Join(", ", bookId)} not found.");
        }

        return book;
    }

    private async Task<User> FindUser(int userId)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(user => userId.Equals(user.Id));

        if (user == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"User with id {string.Join(", ", userId)} not found.");
        }

        return user;
    }
}