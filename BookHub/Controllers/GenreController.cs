using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Core;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.Response;
using WebAPI.Models.Response.Simplified;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GenreController : ControllerBase
{
    private readonly CustomDBContext _dbContext;

    public GenreController(CustomDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    [HttpGet]
    [ProducesResponseType<List<GenreResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetGenres()
    {
        var result = await _dbContext.Genres
            .Include(g => g.BookGenres).ThenInclude(bookGenre => bookGenre.Book)
            .ToListAsync();

        return Ok(result.Select(r => r.Adapt<GenreResponse>() with
        {
            Books = r.BookGenres.Select(ba => ba.Book.Adapt<BookSimplifiedResponse>()).ToList()
        }).ToList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType<GenreResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetGenre(int id)
    {
        var genre = await _dbContext.Genres
            .Where(g => g.Id == id)
            .Include(g => g.BookGenres).ThenInclude(bookGenre => bookGenre.Book)
            .FirstOrDefaultAsync();
        if (genre == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound, "No genre was found.");
        }

        return Ok(genre.Adapt<GenreResponse>() with
        {
            Books = genre.BookGenres.Select(ba => ba.Book.Adapt<BookSimplifiedResponse>()).ToList()
        });
    }

    [HttpPost]
    [ProducesResponseType<GenreResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateGenre([FromBody] GenreModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var books = await FindBooks(model.BookIds);

        var genre = new Genre
        {
            Name = model.Name,
            BookGenres = books.Select(book => new BookGenre { BookId = book.Id }).ToList()
        };

        _dbContext.Genres.Add(genre);
        await _dbContext.SaveChangesAsync();

        return Created($"/Genre/{genre.Id}", genre.Adapt<GenreResponse>() with
        {
            Books = genre.BookGenres.Select(ba => ba.Book.Adapt<BookSimplifiedResponse>()).ToList()
        });
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PatchGenre(int id, [FromBody] GenrePartialModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var genre = await FindGenre(id);

        if (model.BookIds != null)
        {
            await FindBooks(model.BookIds);

            _dbContext.BookGenres.RemoveRange(genre.BookGenres.Where(bg => !model.BookIds.Contains(bg.BookId)));
            var existingBookIds = genre.BookGenres.Select(bg => bg.BookId).ToList();
            _dbContext.BookGenres.AddRange(
                model.BookIds
                    .Except(existingBookIds)
                    .Select(bookId => new BookGenre { GenreId = genre.Id, BookId = bookId })
            );
        }

        genre.Name = model.Name ?? genre.Name;

        _dbContext.Genres.Update(genre);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var genre = await FindGenre(id);
        var books = await FindBooks(model.BookIds);

        genre.Name = model.Name;
        genre.BookGenres = books.Select(book => new BookGenre { GenreId = genre.Id, BookId = book.Id }).ToList();

        _dbContext.Genres.Update(genre);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var genre = await FindGenre(id);

        _dbContext.Genres.Remove(genre);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private async Task<Genre> FindGenre(int genreId)
    {
        var genre = await _dbContext.Genres
            .Where(g => g.Id == genreId)
            .Include(g => g.BookGenres)
            .FirstOrDefaultAsync();
        if (genre == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Genre with id {genreId} not found.");
        }

        return genre;
    }

    private async Task<List<Book>> FindBooks(List<int> bookIds)
    {
        var books = await _dbContext.Books
            .Where(b => bookIds.Contains(b.Id))
            .ToListAsync();
        var missingBookIds = bookIds.Except(books.Select(b => b.Id)).ToList();
        if (missingBookIds.Any())
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Books {{{string.Join(", ", missingBookIds)}}} not found.");
        }

        return books;
    }
}