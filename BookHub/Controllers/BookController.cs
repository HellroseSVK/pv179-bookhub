using BusinessLayer.DTOs.Book;
using BusinessLayer.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Core;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.Response;
using WebAPI.Models.Response.Simplified;

namespace WebAPI.Controllers
{
    public class BookFilter
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }

        public decimal? RatingMin { get; set; }
        public List<int>? AuthorIds { get; set; }
        public List<int>? GenreIds { get; set; }
        public List<int>? PublisherIds { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("filter")]
        [ProducesResponseType<List<BookResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> FilterBooks([FromQuery] BookFilter filter)
        {
            var books = await _bookService.FilterBooksAsync(filter.Adapt<BookFilterDTO>());
            return Ok(books.Adapt<List<BookResponse>>());
        }

        [HttpGet("{id}")]
        [ProducesResponseType<BookResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookAsync(id);

            if (book == null)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound, $"Book with id {id} not found.");
            }

            return Ok(book.Adapt<BookResponse>());
        }

        [HttpPost]
        [ProducesResponseType<BookResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateBook([FromBody] BookModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
            }

            var result = await _bookService.CreateBookAsync(model.Adapt<BookCreateDTO>());

            return Created($"/Book/{result.Id}", result.Adapt<BookResponse>());
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PatchBook(int id, [FromBody] BookPartialModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
            }

            var result = await _bookService.PatchBookAsync(id, model.Adapt<BookPartialDTO>());

            if (!result)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound, $"Book with id {id} not found.");
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
            }

            var result = await _bookService.UpdateBookAsync(id, model.Adapt<BookUpdateDTO>());

            if (!result)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound, $"Book with id {id} not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);

            if (!result)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound, $"Book with id {id} not found.");
            }

            return NoContent();
        }

    }
}