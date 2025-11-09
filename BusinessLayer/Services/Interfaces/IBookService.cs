using BusinessLayer.DTOs.Book;

namespace BusinessLayer.Services.Interfaces;

public interface IBookService
{
    Task<BookDTO> CreateBookAsync(BookCreateDTO book);
    Task<List<BookDTO>> FilterBooksAsync(BookFilterDTO filter);
    Task<BookDTO> GetBookAsync(
        int id,
        bool includeAuthors = true,
        bool includeGenres = true,
        bool includePublisher = true,
        bool includeReviews = true,
        bool includeWishlistItems = false,
        bool includeOrderItems = false);
    Task PatchBookAsync(int id, BookPartialDTO book);
    Task UpdateBookAsync(int id, BookUpdateDTO book);
    Task DeleteBookAsync(int id);
}