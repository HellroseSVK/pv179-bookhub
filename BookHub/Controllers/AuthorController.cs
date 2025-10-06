using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Core;
using WebAPI.Exception;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly CustomDBContext _dbContext;

    public AuthorController(CustomDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    [HttpGet]
    [ProducesResponseType<List<AuthorResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthors()
    {
        var authors = await _dbContext.Authors
            .Include(a => a.BookAuthors)
            .ToListAsync();
        var response = authors.Select(a => new AuthorResponse
        {
            Id = a.Id,
            Name = a.Name,
            Surname = a.Surname,
            CreatedAt = a.CreatedAt,
            BookIds = a.BookAuthors.Select(ba => ba.BookId).ToList()
        }).ToList();
        
        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<AuthorResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthor(int id)
    {
        var author = await _dbContext.Authors
            .Where(a => a.Id == id)
            .Include(a => a.BookAuthors)
            .FirstOrDefaultAsync();
        if (author == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound, "No author was found.");
        }

        var response = new AuthorResponse
        {
            Id = author.Id,
            Name = author.Name,
            Surname = author.Surname,
            CreatedAt = author.CreatedAt,
            BookIds = author.BookAuthors.Select(ba => ba.BookId).ToList()
        };

        return Ok(response);
    }

    [HttpGet("{id}/books")]
    [ProducesResponseType<List<Book>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthorBooks(int id)
    {
        var author = await _dbContext.Authors
            .Where(a => a.Id == id)
            .FirstOrDefaultAsync();
        if (author == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound, "No author was found.");
        }

        var books = await _dbContext.Books
            .Where(b => b.BookAuthors.Any(ba => ba.AuthorId == id))
            .ToListAsync();

        return Ok(books);
    }

    [HttpPost]
    [ProducesResponseType<AuthorResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var books = await FindBooks(model.BookIds);

        var author = new Author
        {
            Name = model.Name,
            Surname = model.Surname
        };
        
        _dbContext.Authors.Add(author);
        await _dbContext.SaveChangesAsync();
        
        if (books.Any())
        {
            _dbContext.BookAuthors.AddRange(books.Select(book => new BookAuthor { AuthorId = author.Id, BookId = book.Id }));
            await _dbContext.SaveChangesAsync();
        }
        
        var response = new AuthorResponse
        {
            Id = author.Id,
            Name = author.Name,
            Surname = author.Surname,
            CreatedAt = author.CreatedAt,
            BookIds = model.BookIds
        };
        
        return Created($"/author/{author.Id}", response);
    }
    
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchAuthor(int id, [FromBody] AuthorPartialModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var author = await FindAuthor(id);
        
        if (model.BookIds != null)
        {
            var books = await FindBooks(model.BookIds);
            
            _dbContext.BookAuthors.RemoveRange(author.BookAuthors.Where(ba => !model.BookIds.Contains(ba.BookId)));
            var existingBookIds = author.BookAuthors.Select(ba => ba.BookId).ToList();
            _dbContext.BookAuthors.AddRange(
                model.BookIds
                    .Except(existingBookIds)
                    .Select(bookId => new BookAuthor { AuthorId = author.Id, BookId = bookId })
            );
        }
        
        author.Name = model.Name ?? author.Name;
        author.Surname = model.Surname ?? author.Surname;
        
        _dbContext.Authors.Update(author);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var author = await FindAuthor(id);
        var books = await FindBooks(model.BookIds);
        
        _dbContext.BookAuthors.RemoveRange(author.BookAuthors.Where(ba => !model.BookIds.Contains(ba.BookId)));
        var existingBookIds = author.BookAuthors.Select(ba => ba.BookId).ToList();
        _dbContext.BookAuthors.AddRange(
            model.BookIds
                .Except(existingBookIds)
                .Select(bookId => new BookAuthor { AuthorId = author.Id, BookId = bookId })
        );
        
        author.Name = model.Name;
        author.Surname = model.Surname;
        
        _dbContext.Authors.Update(author);
        
        await _dbContext.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }
        
        var author = await FindAuthor(id);

        if (author == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound, "No author was found.");
        }
        
        _dbContext.BookAuthors.RemoveRange(author.BookAuthors);
        _dbContext.Authors.Remove(author);
        
        await _dbContext.SaveChangesAsync();
        
        return NoContent();
    }

    private async Task<Author> FindAuthor(int authorId)
    {
        var author = await _dbContext.Authors
            .Where(a => a.Id == authorId)
            .Include(a => a.BookAuthors)
            .FirstOrDefaultAsync();
        if (author == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Author with id {authorId} not found.");
        }

        return author;
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