using BusinessLayer.DTOs.Book;
using BusinessLayer.Facades.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shared.Exception;
using WebAPI.Core;
using WebAPI.Models.Book;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController(IBookFacade bookFacade) : ControllerBase
    {
        [HttpGet("filter")]
        [ProducesResponseType<List<BookResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> FilterBooks([FromQuery] BookFilter filter)
        {
            var books = await bookFacade.FilterBooksAsync(filter.Adapt<BookFilterDTO>());
            return Ok(books.Adapt<List<BookResponse>>());
        }

        [HttpGet("{id}")]
        [ProducesResponseType<BookResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await bookFacade.GetBookAsync(id);
            return Ok(book.Adapt<BookResponse>());
        }

        [HttpPost]
        [ProducesResponseType<BookResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateBook([FromForm] BookWithImageModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
            }

            var bookDto = model.Adapt<BookUpsertDTO>();
            var result = await bookFacade.CreateBookWithImageAsync(bookDto, model.Image);

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

            await bookFacade.PatchBookAsync(id, model.Adapt<BookPartialDTO>());
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

            await bookFacade.UpdateBookAsync(id, model.Adapt<BookUpsertDTO>());
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await bookFacade.DeleteBookAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/image")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UploadBookImage(int id, IFormFile image)
        {
            await bookFacade.UploadBookImageAsync(id, image);
            return NoContent();
        }

        [HttpDelete("{id}/image")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBookImage(int id)
        {
            await bookFacade.DeleteBookImageAsync(id);
            return NoContent();
        }

    }
}