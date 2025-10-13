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
public class PublisherController : ControllerBase
{
    private readonly CustomDBContext _dbContext;

    public PublisherController(CustomDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    [HttpGet]
    [ProducesResponseType<List<PublisherResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPublishers()
    {
        var result = await _dbContext.Publishers
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
    public async Task<IActionResult> GetPublisher(int id)
    {
        var publisher = await _dbContext.Publishers
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

        _dbContext.Publishers.Add(publisher);

        await _dbContext.SaveChangesAsync();

        return Created($"/Publisher/{publisher.Id}", publisher.Adapt<PublisherResponse>() with
        {
            Books = publisher.Books.Select(b => b.Adapt<BookSimplifiedResponse>()).ToList()
        });
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchPublisher(int id, [FromBody] PublisherModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var publisher = FindPublisher(id);
        publisher.Name = model.Name;

        _dbContext.Publishers.Update(publisher);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePublisher(int id, [FromBody] PublisherModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var publisher = FindPublisher(id);
        publisher.Name = model.Name;

        _dbContext.Publishers.Update(publisher);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var publisher = FindPublisher(id);

        if (_dbContext.Books.Any(b => b.PublisherId == id))
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest,
                "Cannot delete publisher with associated books.");
        }

        _dbContext.Publishers.Remove(publisher);

        await _dbContext.SaveChangesAsync();

        return NoContent();
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