using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly CustomDBContext _DBContext;

        public BookController(CustomDBContext dBContext)
        {
            _DBContext = dBContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks([FromQuery] BookFilter filter)
        {
            var query = _DBContext.Books.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Title))
                query = query.Where(p => p.Title.Contains(filter.Title)); // .Contains does partial matching

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(p => p.Description.Contains(filter.Description));

            if (filter.Price.HasValue)
                query = query.Where(p => p.Price == filter.Price.Value);

            if (filter.AuthorId.HasValue)
                query = query.Where(p => p.AuthorId == filter.AuthorId.Value);

            if (filter.GenreId.HasValue)
                query = query.Where(p => p.GenreId == filter.GenreId.Value);

            var result = await query.ToListAsync();

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _DBContext.Books
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound("No book was found.");
            }

            return Ok(book);

        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = new Book()
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                AuthorId = model.AuthorId,
                GenreId = model.GenreId,
                PublisherId = model.PublisherId,
            };

            _DBContext.Books
                .Add(book);

            await _DBContext.SaveChangesAsync();

            return Created();

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _DBContext.Books
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return NotFound("No book was found.");
            }

            // map the model to the entity
            book.Title = model.Title;
            book.Description = model.Description;
            book.Price = model.Price;
            book.AuthorId = model.AuthorId;
            book.GenreId = model.GenreId;
            book.PublisherId = model.PublisherId;

            _DBContext.Books
                .Update(book);

            await _DBContext.SaveChangesAsync();

            return Ok(book);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _DBContext.Books
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return BadRequest("No book was found.");
            }

            _DBContext.Books
                .Remove(book);

            await _DBContext.SaveChangesAsync();

            return NoContent();

        }
    }
}
