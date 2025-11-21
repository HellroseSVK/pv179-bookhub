using BusinessLayer.DTOs.Publisher;
using BusinessLayer.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Core;
using Shared.Exception;
using WebAPI.Models.Publisher;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PublisherController(IPublisherService publisherService) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "User")]
    [ProducesResponseType<List<PublisherResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetPublishers()
    {
        var publishers = await publisherService.GetPublishersAsync();

        return Ok(publishers.Adapt<List<PublisherResponse>>());
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "User")]
    [ProducesResponseType<PublisherResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPublisher(int id)
    {
        var publisher = await publisherService.GetPublisherAsync(id);

        return Ok(publisher.Adapt<PublisherResponse>());
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType<PublisherResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreatePublisher([FromBody] PublisherModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var publisher = await publisherService.CreatePublisherAsync(model.Adapt<PublisherUpsertDTO>());

        return Created($"/Publisher/{publisher.Id}", publisher.Adapt<PublisherResponse>());
    }

    [HttpPatch("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchPublisher(int id, [FromBody] PublisherPartialModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        await publisherService.PatchPublisherAsync(id, model.Adapt<PublisherPartialDTO>());

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdatePublisher(int id, [FromBody] PublisherModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        await publisherService.UpdatePublisherAsync(id, model.Adapt<PublisherUpsertDTO>());

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status403Forbidden)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        await publisherService.DeletePublisherAsync(id);

        return NoContent();
    }
}