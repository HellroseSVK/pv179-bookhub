using BusinessLayer.DTOs.OrderItem;
using BusinessLayer.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Core;
using WebAPI.Exception;
using WebAPI.Models;
using WebAPI.Models.Response;
using WebAPI.Models.Response.Simplified;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderItemController(IOrderItemService orderService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<List<OrderItemResponse>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetOrderItems()
    {
        var orderItems = await orderService.GetOrderItemsAsync();
        return Ok(orderItems.Adapt<List<OrderItemSimplifiedResponse>>());
    }

    [HttpGet("{id}")]
    [ProducesResponseType<OrderItemResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetOrderItem(int id)
    {
        var orderItem = await orderService.GetOrderItemAsync(id);
        return Ok(orderItem.Adapt<OrderItemSimplifiedResponse>());
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

        var result = await orderService.CreateOrderItemAsync(model.Adapt<OrderItemCreateDTO>());
        return Created($"/OrderItem/{result.Id}", result.Adapt<OrderItemSimplifiedResponse>());
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

        await orderService.PatchOrderItemAsync(id, model.Adapt<OrderItemPartialDTO>());
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

        await orderService.UpdateOrderItemAsync(id, model.Adapt<OrderItemUpdateDTO>());
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ErrorResponse>(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        await orderService.DeleteOrderItemAsync(id);
        return NoContent();
    }
}