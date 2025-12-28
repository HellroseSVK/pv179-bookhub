using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Book;
using BusinessLayer.Extensions;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services;

public class BookService(CustomDBContext dbContext) : IBookService
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


    public async Task<List<BookDTO>> FilterBooksAsync(BookFilterDTO filter)
    {
        var books = await dbContext.Books
            .IncludeVirtuals()
            .ApplyBookFilter(filter)
            .Distinct()
            .ToListAsync();
        return books.Adapt<List<BookDTO>>();
    }

    public async Task<PagedResult<BookDTO>> FilterBooksPaginatedAsync(BookFilterDTO filter)
    {
        var query = dbContext.Books
            .IncludeVirtuals()
            .ApplyBookFilter(filter)
            .Distinct();

        var totalCount = await query.CountAsync();

        var books = await query
            .Paginate(filter.PageNumber, filter.PageSize)
            .ToListAsync();

        return new PagedResult<BookDTO>
        {
            Items = books.Adapt<List<BookDTO>>(),
            TotalCount = totalCount,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
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

        if (bookDto.AuthorIds is { Count: > 0 })
        {
            var authors = await FindAuthors(bookDto.AuthorIds);
            var newAuthors = authors.Except(book.BookAuthors.Select(ba => ba.Author));
            book.BookAuthors.AddRange(newAuthors.Select(a => new BookAuthor { Author = a }));
        }

        if (bookDto.GenreIds is { Count: > 0 })
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
        var book = await FindBook(id);

        book.ImagePath = imagePath;
        dbContext.Books.Update(book);
        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveBookImageAsync(int id)
    {
        var book = await FindBook(id);

        book.ImagePath = null;
        dbContext.Books.Update(book);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await FindBook(id);

        dbContext.Books.Remove(book);
        await dbContext.SaveChangesAsync();
    }

    private IQueryable<Book> GetBaseQuery(
        bool includeAuthors,
        bool includeGenres,
        bool includePublisher,
        bool includeReviews,
        bool includeWishlistItems,
        bool includeOrderItems)
    {
        var query = dbContext.Books.AsQueryable();

        if (includeAuthors && includeGenres && includePublisher && includeReviews)
        {
            return query.IncludeVirtuals(includeWishlistItems, includeOrderItems);
        }

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
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Authors with ids {string.Join(", ", missingAuthorIds)} not found.");
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
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Genres  with ids {string.Join(", ", missingGenreIds)} not found.");
        }

        return genres;
    }

    private async Task<Publisher> FindPublisher(int publisherId)
    {
        var publisher = await dbContext.Publishers
            .FirstOrDefaultAsync(p => p.Id == publisherId);

        if (publisher == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound, $"Publisher with id {publisherId} not found.");
        }

        return publisher;
    }
}