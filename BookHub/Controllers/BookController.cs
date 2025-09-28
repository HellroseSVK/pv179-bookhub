using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Core;
using WebAPI.Exception;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class BookFilter
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? AuthorId { get; set; }
        public int? GenreId { get; set; }
        public int? PublisherId { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly CustomDBContext _dbContext;

        public BookController(CustomDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        [HttpGet("filter")]
        [ProducesResponseType<List<Book>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBooks([FromQuery] BookFilter filter)
        {
            var query = _dbContext.Books.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(p => p.Title.Contains(filter.Title)); // .Contains does partial matching

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(p => p.Description.Contains(filter.Description));

            if (filter.Price.HasValue)
                query = query.Where(p => p.Price == filter.Price.Value);

            if (filter.AuthorId.HasValue)
                query = query.Where(p => p.BookAuthors.Any(ba => ba.AuthorId == filter.AuthorId.Value));

            if (filter.GenreId.HasValue)
                query = query.Where(p => p.BookGenres.Any(bg => bg.GenreId == filter.GenreId.Value));
            if (filter.PublisherId.HasValue)
            {
                query = query.Where(p => p.PublisherId == filter.PublisherId.Value);
            }

            var result = await query.ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<List<Book>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _dbContext.Books
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound, "No book was found.");
            }

            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType<List<Book>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateBook(BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authors = FindAuthors(model.AuthorIds);
            var genres = FindGenres(model.GenreIds);
            FindPublisher(model.PublisherId);

            var book = new Book()
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                PublisherId = model.PublisherId,
            };

            _dbContext.Books
                .Add(book);

            _dbContext.BookAuthors.AddRange(authors.Select(a => new BookAuthor { BookId = book.Id, AuthorId = a.Id }));
            _dbContext.BookGenres.AddRange(genres.Select(g => new BookGenre { BookId = book.Id, GenreId = g.Id }));

            await _dbContext.SaveChangesAsync();

            return Created();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType<List<Book>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBook(int id, BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _dbContext.Books
                .Where(x => x.Id == id)
                .Include(book => book.BookAuthors)
                .Include(book => book.BookGenres)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound, "No book was found.");
            }

            FindAuthors(model.AuthorIds);
            FindGenres(model.GenreIds);
            FindPublisher(model.PublisherId);

            _dbContext.BookAuthors.RemoveRange(book.BookAuthors.Where(ba => !model.AuthorIds.Contains(ba.AuthorId)));
            var existingAuthorIds = book.BookAuthors.Select(ba => ba.AuthorId).ToList();
            _dbContext.BookAuthors.AddRange(
                model.AuthorIds
                    .Except(existingAuthorIds)
                    .Select(authorId => new BookAuthor { BookId = book.Id, AuthorId = authorId })
            );

            _dbContext.BookGenres.RemoveRange(book.BookGenres.Where(bg => !model.GenreIds.Contains(bg.GenreId)));
            var existingGenreIds = book.BookGenres.Select(bg => bg.GenreId).ToList();
            _dbContext.BookGenres.AddRange(
                model.GenreIds
                    .Except(existingGenreIds)
                    .Select(genreId => new BookGenre { BookId = book.Id, GenreId = genreId })
            );

            // map the model to the entity
            book.Title = model.Title;
            book.Description = model.Description;
            book.Price = model.Price;
            book.PublisherId = model.PublisherId;

            _dbContext.Books
                .Update(book);

            await _dbContext.SaveChangesAsync();

            return Ok(book);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType<List<Book>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _dbContext.Books
                .Where(x => x.Id == id)
                .Include(book => book.BookAuthors)
                .Include(book => book.BookGenres)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound, "No book was found.");
            }

            _dbContext.BookAuthors.RemoveRange(book.BookAuthors);
            _dbContext.BookGenres.RemoveRange(book.BookGenres);
            _dbContext.Books.Remove(book);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private List<Author> FindAuthors(List<int> authorIds)
        {
            var authors = _dbContext.Authors
                .Where(a => authorIds.Contains(a.Id))
                .ToList();
            var missingAuthorIds = authorIds.Except(authors.Select(a => a.Id)).ToList();
            if (missingAuthorIds.Any())
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound,
                    $"Authors {{{string.Join(", ", missingAuthorIds)}}} not found.");
            }

            return authors;
        }

        private List<Genre> FindGenres(List<int> genreIds)
        {
            var genres = _dbContext.Genres
                .Where(g => genreIds.Contains(g.Id))
                .ToList();
            var missingGenreIds = genreIds.Except(genres.Select(g => g.Id)).ToList();
            if (missingGenreIds.Any())
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound,
                    $"Genres {{{string.Join(", ", missingGenreIds)}}} not found.");
            }

            return genres;
        }

        private Publisher FindPublisher(int publisherId)
        {
            var publisher = _dbContext.Publishers
                .FirstOrDefault(p => p.Id == publisherId);
            if (publisher == null)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound,
                    $"Publisher with id {publisherId} not found.");
            }

            return publisher;
        }
    }
}