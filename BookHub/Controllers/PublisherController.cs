using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;
using WebAPI.Core;
using WebAPI.Models.Book;
using WebAPI.Models.Publisher;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PublisherController(CustomDBContext dBContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<List<PublisherResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetPublishers()
    {
        var result = await dBContext.Publishers
            .Include(publisher => publisher.Books)
            .ToListAsync();

        return Ok(result.Select(r => r.Adapt<PublisherResponse>() with
        {
            Books = r.Books.Select(b => b.Adapt<BookSimplifiedResponse>()).ToList()
        }).ToList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType<PublisherResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetPublisher(int id)
    {
        var publisher = await dBContext.Publishers
            .Where(p => p.Id == id)
            .Include(publisher => publisher.Books)
            .FirstOrDefaultAsync();

        if (publisher == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound, "Publisher not found");
        }

        return Ok(publisher.Adapt<PublisherResponse>() with
        {
            Books = publisher.Books.Select(b => b.Adapt<BookSimplifiedResponse>()).ToList()
        });
    }

    [HttpPost]
    [ProducesResponseType<PublisherResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreatePublisher([FromBody] PublisherModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var publisher = new Publisher
        {
            Name = model.Name
        };

        dBContext.Publishers.Add(publisher);

        await dBContext.SaveChangesAsync();

        return Created($"/Publisher/{publisher.Id}", publisher.Adapt<PublisherResponse>() with
        {
            Books = publisher.Books.Select(b => b.Adapt<BookSimplifiedResponse>()).ToList()
        });
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PatchPublisher(int id, [FromBody] PublisherPartialModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var publisher = FindPublisher(id);
        publisher.Name = model.Name ?? publisher.Name;

        dBContext.Publishers.Update(publisher);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdatePublisher(int id, [FromBody] PublisherModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var publisher = FindPublisher(id);
        publisher.Name = model.Name;

        dBContext.Publishers.Update(publisher);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var publisher = FindPublisher(id);

        if (dBContext.Books.Any(b => b.PublisherId == id))
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest,
                "Cannot delete publisher with associated books.");
        }

        dBContext.Publishers.Remove(publisher);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }

    private Publisher FindPublisher(int publisherId)
    {
        var publisher = dBContext.Publishers
            .FirstOrDefault(p => p.Id == publisherId);
        if (publisher == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Publisher with id {publisherId} not found.");
        }

        return publisher;
    }
}