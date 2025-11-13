using BusinessLayer.DTOs.Book;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services;

public class BookService(CustomDBContext dbContext, IImageService imageService) : IBookService
{
    public async Task<BookDTO> CreateBookAsync(BookUpsertDTO bookDto)
    {
        var authors = await FindAuthors(bookDto.AuthorIds);
        var genres = await FindGenres(bookDto.GenreIds);
        var publisher = await FindPublisher(bookDto.PublisherId);

        var book = bookDto.Adapt<Book>();
        book.BookAuthors = authors.Select(a => new BookAuthor { AuthorId = a.Id, Author = a, Book = book }).ToList();
        book.BookGenres = genres.Select(g => new BookGenre { GenreId = g.Id, Genre = g, Book = book }).ToList();
        book.PublisherId = publisher.Id;

        var created = dbContext.Books.Add(book);
        await dbContext.SaveChangesAsync();

        return created.Entity.Adapt<BookDTO>();
    }

    public async Task<BookDTO> CreateBookWithImageAsync(BookUpsertDTO bookDto, IFormFile? image)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var createdBook = await CreateBookAsync(bookDto);
            
            if (image != null && imageService.IsValidImageFile(image))
            {
                var imagePath = await imageService.SaveBookImageAsync(image, createdBook.Id);
                
                var bookEntity = await FindBook(createdBook.Id, false, false, false, false, false, false);
                bookEntity.ImagePath = imagePath;
                dbContext.Books.Update(bookEntity);
                await dbContext.SaveChangesAsync();
                
                createdBook = bookEntity.Adapt<BookDTO>();
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
        var query = GetBaseQuery(true, true, true, true, false, false);

        if (!string.IsNullOrEmpty(filter.Title))
            query = query.Where(b => b.Title.Contains(filter.Title));

        if (!string.IsNullOrEmpty(filter.Description))
            query = query.Where(b => b.Description.Contains(filter.Description));

        if (filter.PriceMin.HasValue)
            query = query.Where(b => b.Price >= filter.PriceMin.Value);

        if (filter.PriceMax.HasValue)
            query = query.Where(b => b.Price <= filter.PriceMax.Value);

        if (filter.RatingMin.HasValue)
            query = query
                .Where(b => b.Reviews.Any())
                .Where(b => b.Reviews.Average(r => r.Rating) >= (double)filter.RatingMin.Value);

        if (filter.AuthorIds is { Count: > 0 })
            query = query.Where(b => b.BookAuthors.Any(ba => filter.AuthorIds.Contains(ba.AuthorId)));

        if (filter.GenreIds is { Count: > 0 })
            query = query.Where(b => b.BookGenres.Any(bg => filter.GenreIds.Contains(bg.GenreId)));

        if (filter.PublisherIds is { Count: > 0 })
            query = query.Where(b => filter.PublisherIds.Contains(b.PublisherId));

        var books = await query.ToListAsync();
        return books.Adapt<List<BookDTO>>();
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
        var book = await FindBook(id, includeAuthors, includeGenres, includePublisher, includeReviews,
            includeWishlistItems, includeOrderItems);
        return book.Adapt<BookDTO>();
    }

    public async Task PatchBookAsync(int id, BookPartialDTO bookDto)
    {
        var book = await FindBook(id, includeAuthors: true, includeGenres: true);

        book.Title = bookDto.Title ?? book.Title;
        book.Description = bookDto.Description ?? book.Description;
        book.Price = bookDto.Price ?? book.Price;

        if (bookDto.PublisherId.HasValue)
        {
            var publisher = await FindPublisher(bookDto.PublisherId.Value);
            book.PublisherId = publisher.Id;
            book.Publisher = publisher;
        }

        if (bookDto.AuthorIds != null)
        {
            var authors = await FindAuthors(bookDto.AuthorIds);
            var newAuthors = authors.Except(book.BookAuthors.Select(ba => ba.Author));
            book.BookAuthors.AddRange(newAuthors.Select(a => new BookAuthor { Author = a }));
        }

        if (bookDto.GenreIds != null)
        {
            var genres = await FindGenres(bookDto.GenreIds);
            var newGenres = genres.Except(book.BookGenres.Select(bg => bg.Genre));
            book.BookGenres.AddRange(newGenres.Select(g => new BookGenre { Genre = g }));
        }

        dbContext.Books.Update(book);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateBookAsync(int id, BookUpsertDTO bookDto)
    {
        var book = await FindBook(id, includeAuthors: true, includeGenres: true);

        var authors = await FindAuthors(bookDto.AuthorIds);
        var genres = await FindGenres(bookDto.GenreIds);
        var publisher = await FindPublisher(bookDto.PublisherId);

        book.Title = bookDto.Title;
        book.Description = bookDto.Description;
        book.Price = bookDto.Price;
        book.PublisherId = publisher.Id;
        book.Publisher = publisher;

        var assignedAuthors = book.BookAuthors.Select(ba => ba.Author).ToList();
        var oldAuthors = assignedAuthors.Except(authors);
        book.BookAuthors.RemoveAll(ba => oldAuthors.Any(ob => ob != null && ob.Id == ba.AuthorId));
        var newAuthors = authors.Except(assignedAuthors);
        book.BookAuthors.AddRange(newAuthors.Select(author => new BookAuthor { Author = author }));

        var assignedGenres = book.BookGenres.Select(bg => bg.Genre).ToList();
        var oldGenres = assignedGenres.Except(genres);
        book.BookGenres.RemoveAll(bg => oldGenres.Any(og => og != null && og.Id == bg.GenreId));
        var newGenres = genres.Except(assignedGenres);
        book.BookGenres.AddRange(newGenres.Select(genre => new BookGenre { Genre = genre }));

        dbContext.Books.Update(book);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateBookImageAsync(int id, string imagePath)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var book = await FindBook(id, false, false, false, false, false, false);
            
            var oldImagePath = book.ImagePath;
            
            book.ImagePath = imagePath;
            dbContext.Books.Update(book);
            await dbContext.SaveChangesAsync();
            
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

    public async Task UploadBookImageAsync(int id, IFormFile image)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            if (!imageService.IsValidImageFile(image))
            {
                throw new StatusCodeException(StatusCodes.Status400BadRequest, "Invalid image file");
            }

            var book = await FindBook(id, false, false, false, false, false, false);
            var oldImagePath = book.ImagePath;
            
            var newImagePath = await imageService.SaveBookImageAsync(image, id);
            
            book.ImagePath = newImagePath;
            dbContext.Books.Update(book);
            await dbContext.SaveChangesAsync();
            
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
            var book = await FindBook(id, false, false, false, false, false, false);
            
            if (!string.IsNullOrEmpty(book.ImagePath))
            {
                var imagePathToDelete = book.ImagePath;
                
                book.ImagePath = null;
                dbContext.Books.Update(book);
                await dbContext.SaveChangesAsync();
                
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
            var book = await FindBook(id, false, false, false, false, false, false);

            var imagePathToDelete = book.ImagePath;
            
            dbContext.Books.Remove(book);
            await dbContext.SaveChangesAsync();
            
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

    private IQueryable<Book> GetBaseQuery(
        bool includeAuthors,
        bool includeGenres,
        bool includePublisher,
        bool includeReviews,
        bool includeWishlistItems,
        bool includeOrderItems)
    {
        IQueryable<Book> query = dbContext.Books;

        if (includeAuthors)
        {
            query = query.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author);
        }

        if (includeGenres)
        {
            query = query.Include(b => b.BookGenres).ThenInclude(bg => bg.Genre);
        }

        if (includePublisher)
        {
            query = query.Include(b => b.Publisher);
        }

        if (includeReviews)
        {
            query = query.Include(b => b.Reviews);
        }

        if (includeWishlistItems)
        {
            query = query.Include(b => b.WishlistItems);
        }

        if (includeOrderItems)
        {
            query = query.Include(b => b.OrderItems);
        }

        return query;
    }

    private async Task<Book> FindBook(
        int bookId,
        bool includeAuthors = false,
        bool includeGenres = false,
        bool includePublisher = false,
        bool includeReviews = false,
        bool includeWishlistItems = false,
        bool includeOrderItems = false)
    {
        var query = GetBaseQuery(includeAuthors, includeGenres, includePublisher, includeReviews, includeWishlistItems,
            includeOrderItems);
        var book = await query.FirstOrDefaultAsync(b => b.Id == bookId);

        return book ??
               throw new StatusCodeException(StatusCodes.Status404NotFound, $"Book with id {bookId} not found.");
    }

    private async Task<List<Author>> FindAuthors(List<int> authorIds)
    {
        var authors = await dbContext.Authors
            .Where(a => authorIds.Contains(a.Id))
            .ToListAsync();

        var missingAuthorIds = authorIds.Except(authors.Select(a => a.Id)).ToList();
        if (missingAuthorIds.Any())
        {
            throw new InvalidOperationException($"Authors {{{string.Join(", ", missingAuthorIds)}}} not found.");
        }

        return authors;
    }

    private async Task<List<Genre>> FindGenres(List<int> genreIds)
    {
        var genres = await dbContext.Genres
            .Where(g => genreIds.Contains(g.Id))
            .ToListAsync();

        var missingGenreIds = genreIds.Except(genres.Select(g => g.Id)).ToList();
        if (missingGenreIds.Any())
        {
            throw new InvalidOperationException($"Genres {{{string.Join(", ", missingGenreIds)}}} not found.");
        }

        return genres;
    }

    private async Task<Publisher> FindPublisher(int publisherId)
    {
        var publisher = await dbContext.Publishers
            .FirstOrDefaultAsync(p => p.Id == publisherId);

        if (publisher == null)
        {
            throw new InvalidOperationException($"Publisher with id {publisherId} not found.");
        }

        return publisher;
    }
}