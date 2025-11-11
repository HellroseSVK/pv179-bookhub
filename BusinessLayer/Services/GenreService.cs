using BusinessLayer.DTOs.Genre;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services;

public class GenreService(CustomDBContext dBContext) : IGenreService
{
    public async Task<GenreDTO> CreateGenreAsync(GenreCreateDTO genreDto)
    {
        await FindBooks(genreDto.BookIds);

        var created = dBContext.Genres.Add(genreDto.Adapt<Genre>());

        await dBContext.SaveChangesAsync();

        return created.Entity.Adapt<GenreDTO>();
    }

    public async Task<List<GenreDTO>> GetGenreAsync(int[]? ids = null, bool includeBookGenres = true)
    {
        var query = GetBaseQuery(includeBookGenres);

        if (ids is { Length: > 0 })
        {
            query = query.Where(g => ids.Contains(g.Id));
        }

        var genres = await query.ToListAsync();

        return genres.Adapt<List<GenreDTO>>();
    }

    public async Task<GenreDTO> GetGenreAsync(int id, bool includeBookGenres = true)
    {
        var genre = await FindGenre(id, includeBookGenres);

        return genre.Adapt<GenreDTO>();
    }

    public async Task PatchGenreAsync(int id, GenrePartialDTO genreDto)
    {
        var genre = await FindGenre(id, true);

        genre.Name = genreDto.Name ?? genre.Name;

        if (genreDto.BookIds != null)
        {
            var books = await FindBooks(genreDto.BookIds);
            var newBooks = books.Except(genre.BookGenres.Select(bg => bg.Book));
            genre.BookGenres.AddRange(newBooks.Select(book => new BookGenre { Book = book }));
        }

        dBContext.Genres.Update(genre);

        await dBContext.SaveChangesAsync();
    }

    public async Task UpdateGenreAsync(int id, GenreUpdateDTO genreDto)
    {
        var genre = await FindGenre(id, true);

        genre.Name = genreDto.Name;

        var books = await FindBooks(genreDto.BookIds);
        var assignedBooks = genre.BookGenres.Select(bg => bg.Book).ToList();

        var oldBooks = assignedBooks.Except(books).ToList();
        genre.BookGenres.RemoveAll(bg => oldBooks.Any(ob => ob != null && ob.Id == bg.BookId));
        var newBooks = books.Except(assignedBooks).ToList();
        genre.BookGenres.AddRange(newBooks.Select(book => new BookGenre { Book = book }));

        dBContext.Genres.Update(genre);

        await dBContext.SaveChangesAsync();
    }

    public async Task DeleteGenreAsync(int id)
    {
        var genre = await FindGenre(id);

        dBContext.Genres.Remove(genre);

        await dBContext.SaveChangesAsync();
    }

    private async Task<Genre> FindGenre(int genreId, bool includeBookAuthors = false)
    {
        var query = GetBaseQuery(includeBookAuthors);

        var genre = await query.FirstOrDefaultAsync(genre => genre.Id == genreId);

        return genre ??
               throw new StatusCodeException(StatusCodes.Status404NotFound, $"Genre with id {genreId} not found.");
    }

    private IQueryable<Genre> GetBaseQuery(bool includeBookGenres)
    {
        IQueryable<Genre> userQuery = dBContext.Genres;

        if (includeBookGenres)
        {
            userQuery = userQuery
                .Include(genre => genre.BookGenres)
                .ThenInclude(bookGenre => bookGenre.Book);
        }

        return userQuery;
    }

    private async Task<List<Book>> FindBooks(List<int> bookIds)
    {
        var books = await dBContext.Books
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