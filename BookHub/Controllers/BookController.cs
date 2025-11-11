using BusinessLayer.DTOs.Book;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shared.Exception;
using WebAPI.Core;
using WebAPI.Models.Book;

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
    public class BookController(CustomDBContext dBContext, IBookService bookService) : ControllerBase
    {
        [HttpGet("filter")]
        [ProducesResponseType<List<BookResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> FilterBooks([FromQuery] BookFilter filter)
        {
            var books = await bookService.FilterBooksAsync(filter.Adapt<BookFilterDTO>());
            return Ok(books.Adapt<List<BookResponse>>());
        }

        [HttpGet("{id}")]
        [ProducesResponseType<BookResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await bookService.GetBookAsync(id);
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

            var result = await bookService.CreateBookAsync(model.Adapt<BookCreateDTO>());

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

            await bookService.PatchBookAsync(id, model.Adapt<BookPartialDTO>());
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

            await bookService.UpdateBookAsync(id, model.Adapt<BookUpdateDTO>());
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await bookService.DeleteBookAsync(id);
            return NoContent();
        }

    }
}