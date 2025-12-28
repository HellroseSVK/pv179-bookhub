using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Book;

namespace BusinessLayer.Services.Interfaces;

public interface IBookService
{
    Task<BookDTO> CreateBookAsync(BookUpsertDTO bookDto);
    Task<List<BookDTO>> FilterBooksAsync(BookFilterDTO filter);
    Task<PagedResult<BookDTO>> FilterBooksPaginatedAsync(BookFilterDTO filter);
    Task<BookDTO> GetBookAsync(
        int id,
        bool includeAuthors = true,
        bool includeGenres = true,
        bool includePublisher = true,
        bool includeReviews = true,
        bool includeWishlistItems = false,
        bool includeOrderItems = false);
    Task PatchBookAsync(int id, BookPartialDTO bookDto);
    Task UpdateBookAsync(int id, BookUpsertDTO bookDto);
    Task UpdateBookImageAsync(int id, string imagePath);
    Task RemoveBookImageAsync(int id);
    Task DeleteBookAsync(int id);
}