using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;
using WebAPI.Core;
using WebAPI.Models.Book;
using WebAPI.Models.User;
using WebAPI.Models.Wishlist;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WishlistItemController(CustomDBContext dBContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<List<WishlistItemResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetWishlistItems()
    {
        var wishlistItems = await dBContext.WishlistItems
            .Include(wishlistItem => wishlistItem.Book)
            .Include(wishlistItem => wishlistItem.User)
            .ToListAsync();

        return Ok(wishlistItems.Select(wi => wi.Adapt<WishlistItemResponse>() with
        {
            Book = wi.Book.Adapt<BookSimplifiedResponse>(),
            User = wi.User.Adapt<UserSimplifiedResponse>()
        }).ToList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType<WishlistItemResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetWishlistItem(int id)
    {
        var wishlistItem = await FindWishlistItem(id);

        return Ok(wishlistItem.Adapt<WishlistItemResponse>() with
        {
            Book = wishlistItem.Book.Adapt<BookSimplifiedResponse>(),
            User = wishlistItem.User.Adapt<UserSimplifiedResponse>()
        });
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

        var wishlistItem = new WishlistItem
        {
            BookId = model.BookId,
            UserId = model.UserId
        };

        dBContext.WishlistItems.Add(wishlistItem);

        await dBContext.SaveChangesAsync();

        return Created($"/WishlistItem/{wishlistItem.Id}", wishlistItem.Adapt<WishlistItemResponse>() with
        {
            Book = wishlistItem.Book.Adapt<BookSimplifiedResponse>(),
            User = wishlistItem.User.Adapt<UserSimplifiedResponse>()
        });
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

        var wishlistItem = await FindWishlistItem(id);

        wishlistItem.BookId = model.BookId ?? wishlistItem.BookId;
        wishlistItem.UserId = model.UserId ?? wishlistItem.UserId;

        dBContext.WishlistItems.Update(wishlistItem);

        await dBContext.SaveChangesAsync();

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

        var wishlistItem = await FindWishlistItem(id);

        wishlistItem.BookId = model.BookId;
        wishlistItem.UserId = model.UserId;

        dBContext.WishlistItems.Update(wishlistItem);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteWishlistItem(int id)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var wishlistItem = await FindWishlistItem(id);

        dBContext.WishlistItems.Remove(wishlistItem);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }

    private async Task<WishlistItem> FindWishlistItem(int WishlistItemId)
    {
        var wishlistItem = await dBContext.WishlistItems
            .FirstOrDefaultAsync(p => p.Id == WishlistItemId);

        if (wishlistItem == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"WishlistItem with id {WishlistItemId} not found.");
        }

        return wishlistItem;
    }
}