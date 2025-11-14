using BusinessLayer.DTOs.Book;
using BusinessLayer.Facades.Interfaces;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Shared.Exception;

namespace BusinessLayer.Facades;

public class BookFacade(IBookService bookService, IImageService imageService, CustomDBContext dbContext) : IBookFacade
{
    public async Task<BookDTO> CreateBookWithImageAsync(BookUpsertDTO bookDto, IFormFile? image)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var createdBook = await bookService.CreateBookAsync(bookDto);
            
            if (image != null && imageService.IsValidImageFile(image))
            {
                var imagePath = await imageService.SaveBookImageAsync(image, createdBook.Id);
                
                await bookService.UpdateBookImageAsync(createdBook.Id, imagePath);
                
                createdBook = await bookService.GetBookAsync(createdBook.Id, includeWishlistItems: false, includeOrderItems: false);
            }
            
            await transaction.CommitAsync();
            return createdBook;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<BookDTO>> FilterBooksAsync(BookFilterDTO filter)
    {
        return await bookService.FilterBooksAsync(filter);
    }

    public async Task<BookDTO> GetBookAsync(
        int id,
        bool includeAuthors = true,
        bool includeGenres = true,
        bool includePublisher = true,
        bool includeReviews = true,
        bool includeWishlistItems = false,
        bool includeOrderItems = false)
    {
        return await bookService.GetBookAsync(id, includeAuthors, includeGenres, includePublisher, includeReviews, includeWishlistItems, includeOrderItems);
    }

    public async Task PatchBookAsync(int id, BookPartialDTO book)
    {
        await bookService.PatchBookAsync(id, book);
    }

    public async Task UpdateBookAsync(int id, BookUpsertDTO book)
    {
        await bookService.UpdateBookAsync(id, book);
    }

    public async Task UploadBookImageAsync(int id, IFormFile image)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            if (!imageService.IsValidImageFile(image))
            {
                throw new StatusCodeException(StatusCodes.Status400BadRequest, "Invalid image file");
            }

            string? oldImagePath = await GetCurrentImagePathAsync(id);

            var newImagePath = await imageService.SaveBookImageAsync(image, id);
            
            await bookService.UpdateBookImageAsync(id, newImagePath);
            
            if (!string.IsNullOrEmpty(oldImagePath))
            {
                await imageService.DeleteBookImageAsync(oldImagePath);
            }
            
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteBookImageAsync(int id)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            string? imagePathToDelete = await GetCurrentImagePathAsync(id);
            
            await bookService.RemoveBookImageAsync(id);
            
            if (!string.IsNullOrEmpty(imagePathToDelete))
            {
                await imageService.DeleteBookImageAsync(imagePathToDelete);
            }
            
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteBookAsync(int id)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            string? imagePathToDelete = await GetCurrentImagePathAsync(id);
            
            await bookService.DeleteBookAsync(id);
            
            if (!string.IsNullOrEmpty(imagePathToDelete))
            {
                await imageService.DeleteBookImageAsync(imagePathToDelete);
            }

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task<string?> GetCurrentImagePathAsync(int bookId)
    {
        var currentBook = await bookService.GetBookAsync(bookId, includeAuthors: false, includeGenres: false,
            includePublisher: false, includeReviews: false, includeWishlistItems: false, includeOrderItems: false);
        
        return string.IsNullOrEmpty(currentBook.ImageUrl) 
            ? null 
            : Path.GetFileName(currentBook.ImageUrl);
    }
}