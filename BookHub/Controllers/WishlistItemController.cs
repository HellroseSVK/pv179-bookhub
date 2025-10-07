using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Core;
using WebAPI.Exception;
using WebAPI.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WishlistItemController : ControllerBase
{
    private readonly CustomDBContext _dbContext;

    public WishlistItemController(CustomDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    [HttpGet]
    [ProducesResponseType<List<WishlistItem>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWishlistItems()
    {
        var wishlistItems = await _dbContext.WishlistItems.ToListAsync();

        return Ok(wishlistItems);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<WishlistItem>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetWishlistItem(int id)
    {
        var wishlistItem = await FindWishlistItem(id);

        return Ok(wishlistItem);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateWishlistItem([FromBody] WishlistItemModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var wishlistItem = new WishlistItem
        {
            BookId = model.BookId,
            UserId = model.UserId
        };

        _dbContext.WishlistItems.Add(wishlistItem);

        await _dbContext.SaveChangesAsync();

        return Created();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchWishlistItem(int id, [FromBody] WishlistItemPartialModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var wishlistItem = await FindWishlistItem(id);

        wishlistItem.BookId = model.BookId ?? wishlistItem.BookId;
        wishlistItem.UserId = model.UserId ?? wishlistItem.UserId;

        _dbContext.WishlistItems.Update(wishlistItem);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateWishlistItem(int id, [FromBody] WishlistItemModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var wishlistItem = await FindWishlistItem(id);

        wishlistItem.BookId = model.BookId;
        wishlistItem.UserId = model.UserId;

        _dbContext.WishlistItems.Update(wishlistItem);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteWishlistItem(int id)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var wishlistItem = await FindWishlistItem(id);

        _dbContext.WishlistItems.Remove(wishlistItem);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private async Task<WishlistItem> FindWishlistItem(int WishlistItemId)
    {
        var wishlistItem = await _dbContext.WishlistItems
            .FirstOrDefaultAsync(p => p.Id == WishlistItemId);

        if (wishlistItem == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"WishlistItem with id {WishlistItemId} not found.");
        }

        return wishlistItem;
    }
}