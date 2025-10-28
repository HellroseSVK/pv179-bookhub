using BusinessLayer.DTOs.Book;

namespace BusinessLayer.Services;

public interface IBookService
{
    Task<BookDTO> CreateBookAsync(BookCreateDTO book);
    Task<List<BookDTO>> FilterBooksAsync(BookFilterDTO filter);
    Task<BookDTO?> GetBookAsync(
        int id,
        bool includeAuthors = true,
        bool includeGenres = true,
        bool includePublisher = true,
        bool includeReviews = true,
        bool includeWishlistItems = false,
        bool includeOrderItems = false);
    Task<bool> PatchBookAsync(int id, BookPartialDTO book);
    Task<bool> UpdateBookAsync(int id, BookUpdateDTO book);
    Task<bool> DeleteBookAsync(int id);
}