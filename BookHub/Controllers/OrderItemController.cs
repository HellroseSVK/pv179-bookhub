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
public class OrderItemController : ControllerBase
{
    private readonly CustomDBContext _dbContext;

    public OrderItemController(CustomDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    [HttpGet]
    [ProducesResponseType<List<OrderItem>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrderItems()
    {
        var orderItems = await _dbContext.OrderItems.ToListAsync();

        return Ok(orderItems);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<OrderItem>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrderItem(int id)
    {
        var orderItem = await (FindOrderItem(id));

        return Ok(orderItem);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
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

        _dbContext.OrderItems.Add(orderItem);

        await _dbContext.SaveChangesAsync();

        return Created();
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PatchOrderItem(int id, [FromBody] OrderItemPartialModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var orderItem = await FindOrderItem(id);

        orderItem.BookId = model.BookId ?? orderItem.BookId;
        orderItem.UserId = model.UserId ?? orderItem.UserId;

        _dbContext.OrderItems.Update(orderItem);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItemModel model)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var orderItem = await FindOrderItem(id);

        orderItem.BookId = model.BookId;
        orderItem.UserId = model.UserId;

        _dbContext.OrderItems.Update(orderItem);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        if (!ModelState.IsValid)
        {
            throw new StatusCodeException(StatusCodes.Status400BadRequest, ModelState.ToString() ?? string.Empty);
        }

        var orderItem = await FindOrderItem(id);

        _dbContext.OrderItems.Remove(orderItem);

        await _dbContext.SaveChangesAsync();

        return NoContent();
    }

    private async Task<OrderItem> FindOrderItem(int OrderItemId)
    {
        var orderItem = await _dbContext.OrderItems
            .FirstOrDefaultAsync(p => p.Id == OrderItemId);

        if (orderItem == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"OrderItem with id {OrderItemId} not found.");
        }

        return orderItem;
    }
}