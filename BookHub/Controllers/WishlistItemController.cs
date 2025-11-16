using BusinessLayer.DTOs.WishlistItem;
using BusinessLayer.Services;
using BusinessLayer.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shared.Exception;
using WebAPI.Core;
using WebAPI.Models.Wishlist;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WishlistItemController(IWishlistItemService wishlistService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<List<WishlistItemResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetWishlistItems()
    {
        var wishlistItems = await wishlistService.GetWishlistItemsAsync();

        return Ok(wishlistItems.Adapt<List<WishlistItemResponse>>());
    }

    [HttpGet("{id}")]
    [ProducesResponseType<WishlistItemResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetWishlistItem(int id)
    {
        var wishlistItem = await wishlistService.GetWishlistItemAsync(id);

        return Ok(wishlistItem.Adapt<WishlistItemResponse>());

    }

    [HttpPost]
    [ProducesResponseType<WishlistItemResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateWishlistItem([FromBody] WishlistItemModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var result = await wishlistService.CreateWishlistItemAsync(model.Adapt<WishlistItemUpsertDTO>());

        return Created($"/WishlistItem/{result.Id}", result.Adapt<WishlistItemResponse>());
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PatchWishlistItem(int id, [FromBody] WishlistItemPartialModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        await wishlistService.PatchWishlistItemAsync(id, model.Adapt<WishlistItemPartialDTO>());

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateWishlistItem(int id, [FromBody] WishlistItemModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        await wishlistService.UpdateWishlistItemAsync(id, model.Adapt<WishlistItemUpsertDTO>());

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteWishlistItem(int id)
    {
        await wishlistService.DeleteWishlistItemAsync(id);

        return NoContent();
    }
}