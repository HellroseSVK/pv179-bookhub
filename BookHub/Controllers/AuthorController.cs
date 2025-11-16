using BusinessLayer.DTOs.Author;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;
using WebAPI.Core;
using WebAPI.Models.Author;
using WebAPI.Models.Book;
using WebAPI.Models.Genre;
using WebAPI.Models.Publisher;
using WebAPI.Models.Review;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController(CustomDBContext dBContext, IAuthorService authorService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<List<AuthorResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAuthors()
    {
        var authors = await authorService.GetAuthorsAsync(includeBookAuthors: true);

        return Ok(authors.Adapt<List<AuthorResponse>>());
    }

    [HttpGet("{id}")]
    [ProducesResponseType<AuthorResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAuthor(int id)
    {
        var author = await authorService.GetAuthorAsync(id);

        return Ok(author.Adapt<AuthorResponse>());
    }

    [HttpGet("{id}/books")]
    [ProducesResponseType<List<BookResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAuthorBooks(int id)
    {
        var author = await dBContext.Authors
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();
        if (author == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound, "No author was found.");
        }

        var books = await dBContext.Books
            .Where(b => b.BookAuthors.Any(ba => ba.AuthorId == id))
            .Include(book => book.BookAuthors).ThenInclude(bookAuthor => bookAuthor.Author)
            .Include(book => book.BookGenres).ThenInclude(bookGenre => bookGenre.Genre)
            .Include(book => book.Reviews)
            .Include(book => book.Publisher)
            .ToListAsync();

        return Ok(books.Select(b => b.Adapt<BookResponse>() with
        {
            Authors = b.BookAuthors.Select(ba => ba.Author.Adapt<AuthorSimplifiedResponse>()).ToList(),
            Genres = b.BookGenres.Select(bg => bg.Genre.Adapt<GenreSimplifiedResponse>()).ToList(),
            Reviews = b.Reviews.Select(rv => rv.Adapt<ReviewSimplifiedResponse>()).ToList(),
            Publisher = b.Publisher.Adapt<PublisherSimplifiedResponse>()
        }).ToList());
    }

    [HttpPost]
    [ProducesResponseType<AuthorResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var author = await authorService.CreateAuthorAsync(model.Adapt<AuthorUpsertDTO>());

        return Created($"/Author/{author.Id}", author.Adapt<AuthorResponse>());
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PatchAuthor(int id, [FromBody] AuthorPartialModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        await authorService.PatchAuthorAsync(id, model.Adapt<AuthorPartialDTO>());

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        await authorService.UpdateAuthorAsync(id, model.Adapt<AuthorUpsertDTO>());

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        await authorService.DeleteAuthorAsync(id);

        return NoContent();
    }
}