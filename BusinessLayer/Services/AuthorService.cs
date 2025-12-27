using BusinessLayer.DTOs.Author;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services;

public class AuthorService(CustomDBContext dbContext) : IAuthorService
{
    public async Task<AuthorDTO> CreateAuthorAsync(AuthorUpsertDTO authorDto)
    {
        var books = await FindBooks(authorDto.BookIds);

        var author = authorDto.Adapt<Author>();
        author.BookAuthors = books.Select(b => new BookAuthor { BookId = b.Id, Book = b, Author = author }).ToList();

        var created = dbContext.Authors.Add(author);

        await dbContext.SaveChangesAsync();

        return created.Entity.Adapt<AuthorDTO>();
    }

    public async Task<List<AuthorDTO>> GetAuthorsAsync(int[]? ids = null, bool includeBookAuthors = true)
    {
        var query = GetBaseQuery(includeBookAuthors);

        if (ids is { Length: > 0 })
        {
            query = query.Where(a => ids.Contains(a.Id));
        }

        var authors = await query.ToListAsync();

        return authors.Adapt<List<AuthorDTO>>();
    }

    public async Task<AuthorDTO> GetAuthorAsync(int id, bool includeBookAuthors = true)
    {
        var author = await FindAuthor(id, includeBookAuthors);

        return author.Adapt<AuthorDTO>();
    }

    public async Task<List<AuthorDTO>> SearchAuthorsAsync(string searchTerm)
    {
        var query = GetBaseQuery(false);
        
        if (!string.IsNullOrEmpty(searchTerm))
        {
            var searchTermLower = searchTerm.ToLower();
            query = query.Where(a => (a.Name + " " + a.Surname).ToLower().Contains(searchTermLower) ||
                                   (a.Surname + " " + a.Name).ToLower().Contains(searchTermLower) ||
                                   a.Name.ToLower().Contains(searchTermLower) || 
                                   a.Surname.ToLower().Contains(searchTermLower));
        }
        
        var authors = await query.ToListAsync();
        return authors.Adapt<List<AuthorDTO>>();
    }

    public async Task PatchAuthorAsync(int id, AuthorPartialDTO authorDto)
    {
        var author = await FindAuthor(id, true);

        author.Name = authorDto.Name ?? author.Name;
        author.Surname = authorDto.Surname ?? author.Surname;

        if (authorDto.BookIds != null)
        {
            var books = await FindBooks(authorDto.BookIds);
            var newBooks = books.Except(author.BookAuthors.Select(ba => ba.Book));
            author.BookAuthors.AddRange(newBooks.Select(book => new BookAuthor { Book = book }));
        }

        dbContext.Authors.Update(author);

        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAuthorAsync(int id, AuthorUpsertDTO authorDto)
    {
        var author = await FindAuthor(id, true);

        author.Name = authorDto.Name;
        author.Surname = authorDto.Surname;

        var books = await FindBooks(authorDto.BookIds);
        var assignedBooks = author.BookAuthors.Select(ba => ba.Book).ToList();

        var oldBooks = assignedBooks.Except(books);
        author.BookAuthors.RemoveAll(ba => oldBooks.Any(ob => ob != null && ob.Id == ba.BookId));
        var newBooks = books.Except(assignedBooks);
        author.BookAuthors.AddRange(newBooks.Select(book => new BookAuthor { Book = book }));

        dbContext.Authors.Update(author);

        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAuthorAsync(int id)
    {
        var author = await FindAuthor(id);

        dbContext.Authors.Remove(author);

        await dbContext.SaveChangesAsync();
    }

    private async Task<Author> FindAuthor(int authorId, bool includeBookAuthors = false)
    {
        var query = GetBaseQuery(includeBookAuthors);

        var author = await query.FirstOrDefaultAsync(author => author.Id == authorId);

        return author ??
               throw new StatusCodeException(StatusCodes.Status404NotFound, $"Author with id {authorId} not found.");
    }

    private IQueryable<Author> GetBaseQuery(bool includeBookAuthors)
    {
        IQueryable<Author> userQuery = dbContext.Authors;

        if (includeBookAuthors)
        {
            userQuery = userQuery
                .Include(author => author.BookAuthors)
                .ThenInclude(bookAuthor => bookAuthor.Book);
        }

        return userQuery;
    }

    private async Task<List<Book>> FindBooks(List<int> bookIds)
    {
        var books = await dbContext.Books
            .Where(book => bookIds.Contains(book.Id))
            .ToListAsync();

        var missingBookIds = bookIds.Except(books.Select(b => b.Id)).ToList();
        if (missingBookIds.Any())
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Books with ids {string.Join(", ", missingBookIds)} not found.");
        }

        return books;
    }
}