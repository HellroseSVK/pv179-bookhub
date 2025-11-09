using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;
using WebAPI.Core;
using WebAPI.Models.Book;
using WebAPI.Models.Order;
using WebAPI.Models.User;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderItemController(CustomDBContext dBContext) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<List<OrderItemResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetOrderItems()
    {
        var orderItems = await dBContext.OrderItems
            .Include(orderItem => orderItem.Book)
            .Include(orderItem => orderItem.User)
            .ToListAsync();

        return Ok(orderItems.Select(ot => ot.Adapt<OrderItemResponse>() with
        {
            Book = ot.Book.Adapt<BookSimplifiedResponse>(),
            User = ot.User.Adapt<UserSimplifiedResponse>()
        }).ToList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType<OrderItemResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetOrderItem(int id)
    {
        var orderItem = await (FindOrderItem(id));

        return Ok(orderItem.Adapt<OrderItemResponse>() with
        {
            Book = orderItem.Book.Adapt<BookSimplifiedResponse>(),
            User = orderItem.User.Adapt<UserSimplifiedResponse>()
        });
    }

    [HttpPost]
    [ProducesResponseType<OrderItemResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateOrderItem([FromBody] OrderItemModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var orderItem = new OrderItem
        {
            BookId = model.BookId,
            UserId = model.UserId
        };

        dBContext.OrderItems.Add(orderItem);

        await dBContext.SaveChangesAsync();

        return Created($"/OrderItem/{orderItem.Id}", orderItem.Adapt<OrderItemResponse>() with
        {
            Book = orderItem.Book.Adapt<BookSimplifiedResponse>(),
            User = orderItem.User.Adapt<UserSimplifiedResponse>()
        });
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> PatchOrderItem(int id, [FromBody] OrderItemPartialModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var orderItem = await FindOrderItem(id);

        orderItem.BookId = model.BookId ?? orderItem.BookId;
        orderItem.UserId = model.UserId ?? orderItem.UserId;

        dBContext.OrderItems.Update(orderItem);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItemModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var orderItem = await FindOrderItem(id);

        orderItem.BookId = model.BookId;
        orderItem.UserId = model.UserId;

        dBContext.OrderItems.Update(orderItem);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var orderItem = await FindOrderItem(id);

        dBContext.OrderItems.Remove(orderItem);

        await dBContext.SaveChangesAsync();

        return NoContent();
    }

    private async Task<OrderItem> FindOrderItem(int OrderItemId)
    {
        var orderItem = await dBContext.OrderItems
            .FirstOrDefaultAsync(p => p.Id == OrderItemId);

        if (orderItem == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"OrderItem with id {OrderItemId} not found.");
        }

        return orderItem;
    }
}