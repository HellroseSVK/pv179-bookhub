using BusinessLayer.DTOs.Publisher;
using BusinessLayer.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shared.Exception;
using WebAPI.Core;
using WebAPI.Models.Publisher;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PublisherController(IPublisherService publisherService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<List<PublisherResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetPublishers()
    {
        var publishers = await publisherService.GetAuthorsAsync();

        return Ok(publishers.Adapt<List<PublisherResponse>>());
    }

    [HttpGet("{id}")]
    [ProducesResponseType<PublisherResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetPublisher(int id)
    {
        var publisher = await publisherService.GetAuthorAsync(id);

        return Ok(publisher.Adapt<PublisherResponse>());
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

        var publisher = await publisherService.CreateAuthorAsync(model.Adapt<PublisherUpsertDTO>());

        return Created($"/Publisher/{publisher.Id}", publisher.Adapt<PublisherResponse>());
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

        await publisherService.PatchAuthorAsync(id, model.Adapt<PublisherPartialDTO>());

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

        await publisherService.UpdateAuthorAsync(id, model.Adapt<PublisherUpsertDTO>());

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

        await publisherService.DeleteAuthorAsync(id);

        return NoContent();
    }
}