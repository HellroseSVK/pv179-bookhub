using BusinessLayer.DTOs.Book;
using Microsoft.AspNetCore.Http;

namespace BusinessLayer.Facades.Interfaces;

public interface IBookFacade
{
    Task<BookDTO> CreateBookWithImageAsync(BookUpsertDTO book, IFormFile? image);
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
    Task UpdateBookAsync(int id, BookUpsertDTO book);
    Task UploadBookImageAsync(int id, IFormFile image);
    Task DeleteBookImageAsync(int id);
    Task DeleteBookAsync(int id);
}