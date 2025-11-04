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

namespace WebAPI.Controllers
{
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
        [ProducesResponseType<List<BookResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> FilterBooks([FromQuery] BookFilter filter)
        {
            var query = _dbContext.Books.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(p => p.Title.Contains(filter.Title)); // .Contains does partial matching

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(p => p.Description.Contains(filter.Description));

            if (filter.PriceMin.HasValue)
                query = query.Where(p => p.Price >= filter.PriceMin.Value);

            if (filter.PriceMax.HasValue)
                query = query.Where(p => p.Price <= filter.PriceMax.Value);

            if (filter.RatingMin.HasValue)
                query = query
                    .Where(p => p.Reviews.Any()) // Ensure there is at least one review to avoid division by zero
                    .Where(p => p.Reviews.Average(r => r.Rating) >= (double)filter.RatingMin.Value);

            if (filter.AuthorIds is { Count: > 0 })
                query = query.Where(p => p.BookAuthors.Any(ba => filter.AuthorIds.Contains(ba.AuthorId)));

            if (filter.GenreIds is { Count: > 0 })
                query = query.Where(p => p.BookGenres.Any(bg => filter.GenreIds.Contains(bg.GenreId)));

            if (filter.PublisherIds is { Count: > 0 })
                query = query.Where(p => filter.PublisherIds.Contains(p.PublisherId));

            var result = await query
                .Include(book => book.BookAuthors).ThenInclude(bookAuthor => bookAuthor.Author)
                .Include(book => book.BookGenres).ThenInclude(bookGenre => bookGenre.Genre)
                .Include(book => book.Reviews)
                .Include(book => book.Publisher)
                .ToListAsync();

            return Ok(result.Select(r => r.Adapt<BookResponse>() with
            {
                Authors = r.BookAuthors.Select(ba => ba.Author.Adapt<AuthorSimplifiedResponse>()).ToList(),
                Genres = r.BookGenres.Select(bg => bg.Genre.Adapt<GenreSimplifiedResponse>()).ToList(),
                Reviews = r.Reviews.Select(rv => rv.Adapt<ReviewSimplifiedResponse>()).ToList(),
                Publisher = r.Publisher.Adapt<PublisherSimplifiedResponse>()
            }).ToList());
        }

        [HttpGet("{id}")]
        [ProducesResponseType<BookResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _dbContext.Books
                .Where(x => x.Id == id)
                .Include(book => book.BookAuthors).ThenInclude(bookAuthor => bookAuthor.Author)
                .Include(book => book.BookGenres).ThenInclude(bookGenre => bookGenre.Genre)
                .Include(book => book.Reviews)
                .Include(book => book.Publisher)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound, "No book was found.");
            }

            return Ok(book.Adapt<BookResponse>() with
            {
                Authors = book.BookAuthors.Select(ba => ba.Author.Adapt<AuthorSimplifiedResponse>()).ToList(),
                Genres = book.BookGenres.Select(bg => bg.Genre.Adapt<GenreSimplifiedResponse>()).ToList(),
                Reviews = book.Reviews.Select(rv => rv.Adapt<ReviewSimplifiedResponse>()).ToList(),
                Publisher = book.Publisher.Adapt<PublisherSimplifiedResponse>()
            });
        }

        [HttpPost]
        [ProducesResponseType<BookResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateBook(BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authors = await FindAuthors(model.AuthorIds);
            var genres = await FindGenres(model.GenreIds);
            var publisher = await FindPublisher(model.PublisherId);

            var book = new Book()
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                PublisherId = model.PublisherId,
                Publisher = publisher,
                BookAuthors = authors.Select(author => new BookAuthor { Author = author, AuthorId = author.Id })
                    .ToList(),
                BookGenres = genres.Select(genre => new BookGenre { Genre = genre, GenreId = genre.Id }).ToList()
            };

            _dbContext.Books.Add(book);

            await _dbContext.SaveChangesAsync();

            return Created($"/Book/{book.Id}", book.Adapt<BookResponse>() with
            {
                Authors = book.BookAuthors.Select(ba => ba.Author.Adapt<AuthorSimplifiedResponse>()).ToList(),
                Genres = book.BookGenres.Select(bg => bg.Genre.Adapt<GenreSimplifiedResponse>()).ToList(),
                Reviews = book.Reviews.Select(rv => rv.Adapt<ReviewSimplifiedResponse>()).ToList(),
                Publisher = book.Publisher.Adapt<PublisherSimplifiedResponse>()
            });
        }

        [HttpPatch]
        [Route("{id}")]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PatchBook(int id, BookModel model)
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

            var allAuthors = await FindAuthors(model.AuthorIds);
            var allGenres = await FindGenres(model.GenreIds);
            var publisher = await FindPublisher(model.PublisherId);

            // map the model to the entity
            book.Title = model.Title;
            book.Description = model.Description;
            book.Price = model.Price;
            book.PublisherId = publisher.Id;
            book.Publisher = publisher;

            book.BookAuthors.AddRange(allAuthors
                .Where(a => book.BookAuthors.All(ba => ba.AuthorId != a.Id))
                .Select(a => new BookAuthor { AuthorId = a.Id, Author = a }));
            book.BookGenres.AddRange(allGenres
                .Where(g => book.BookGenres.All(bg => bg.GenreId != g.Id))
                .Select(g => new BookGenre { GenreId = g.Id, Genre = g }));


            _dbContext.Books.Update(book);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
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

            var allAuthors = await FindAuthors(model.AuthorIds);
            var allGenres = await FindGenres(model.GenreIds);
            var publisher = await FindPublisher(model.PublisherId);

            // map the model to the entity
            book.Title = model.Title;
            book.Description = model.Description;
            book.Price = model.Price;
            book.PublisherId = publisher.Id;
            book.Publisher = publisher;

            book.BookAuthors = book.BookAuthors.Where(ba => allAuthors.Any(a => a.Id == ba.AuthorId)).ToList();
            book.BookAuthors.AddRange(allAuthors
                .Where(a => book.BookAuthors.All(ba => ba.AuthorId != a.Id))
                .Select(a => new BookAuthor { AuthorId = a.Id, Author = a }));
            book.BookGenres = book.BookGenres.Where(bg => allGenres.Any(a => a.Id == bg.GenreId)).ToList();
            book.BookGenres.AddRange(allGenres
                .Where(g => book.BookGenres.All(bg => bg.GenreId != g.Id))
                .Select(g => new BookGenre { GenreId = g.Id, Genre = g }));


            _dbContext.Books.Update(book);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
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

            _dbContext.Books.Remove(book);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private async Task<List<Author>> FindAuthors(List<int> authorIds)
        {
            var authors = await _dbContext.Authors
                .Where(a => authorIds.Contains(a.Id))
                .ToListAsync();
            var missingAuthorIds = authorIds.Except(authors.Select(a => a.Id)).ToList();
            if (missingAuthorIds.Any())
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound,
                    $"Authors {{{string.Join(", ", missingAuthorIds)}}} not found.");
            }

            return authors;
        }

        private async Task<List<Genre>> FindGenres(List<int> genreIds)
        {
            var genres = await _dbContext.Genres
                .Where(g => genreIds.Contains(g.Id))
                .ToListAsync();
            var missingGenreIds = genreIds.Except(genres.Select(g => g.Id)).ToList();
            if (missingGenreIds.Any())
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound,
                    $"Genres {{{string.Join(", ", missingGenreIds)}}} not found.");
            }

            return genres;
        }

        private async Task<Publisher> FindPublisher(int publisherId)
        {
            var publisher = await _dbContext.Publishers
                .FirstOrDefaultAsync(p => p.Id == publisherId);
            if (publisher == null)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound,
                    $"Publisher with id {publisherId} not found.");
            }

            return publisher;
        }
    }
}