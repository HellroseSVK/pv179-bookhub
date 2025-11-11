using BusinessLayer.DTOs.OrderItem;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services;

public class OrderItemService(CustomDBContext dbContext) : IOrderItemService
{
    public async Task<OrderItemDTO> CreateOrderItemAsync(OrderItemUpsertDTO orderItemDto)
    {
        var created = dbContext.OrderItems.Add(orderItemDto.Adapt<OrderItem>());
        await dbContext.SaveChangesAsync();

        return created.Entity.Adapt<OrderItemDTO>();
    }

    public async Task<OrderItemDTO> GetOrderItemAsync(int id, bool includeBook = true, bool includeUser = true)
    {
        var orderItem = await FindOrderItem(id, includeBook, includeUser);
        return orderItem.Adapt<OrderItemDTO>();
    }

    public async Task<List<OrderItemDTO>> GetOrderItemsAsync(int[]? ids = null, bool includeBook = true, bool includeUser = true)
    {
        var query = GetBaseQuery(includeBook, includeUser);

        if (ids is { Length: > 0 })
        {
            query = query.Where(oi => ids.Contains(oi.Id));
        }

        var orderItems = await query.ToListAsync();
        return orderItems.Adapt<List<OrderItemDTO>>();
    }

    public async Task PatchOrderItemAsync(int id, OrderItemPartialDTO orderItemDto)
    {
        var orderItem = await FindOrderItem(id);

        orderItem.BookId = orderItemDto.BookId ?? orderItem.BookId;
        orderItem.UserId = orderItemDto.UserId ?? orderItem.UserId;

        dbContext.OrderItems.Update(orderItem);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateOrderItemAsync(int id, OrderItemUpsertDTO orderItemDto)
    {
        var orderItem = await FindOrderItem(id);

        orderItem.BookId = orderItemDto.BookId;
        orderItem.UserId = orderItemDto.UserId;

        dbContext.OrderItems.Update(orderItem);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteOrderItemAsync(int id)
    {
        var orderItem = await FindOrderItem(id);
        
        dbContext.OrderItems.Remove(orderItem);
        await dbContext.SaveChangesAsync();
    }

    private async Task<OrderItem> FindOrderItem(int orderItemId, bool includeBook = false, bool includeUser = false)
    {
        var query = GetBaseQuery(includeBook, includeUser);
        var orderItem = await query.FirstOrDefaultAsync(oi => oi.Id == orderItemId);

        return orderItem ??
               throw new StatusCodeException(StatusCodes.Status404NotFound, $"OrderItem with id {orderItemId} not found.");
    }

    private IQueryable<OrderItem> GetBaseQuery(bool includeBook, bool includeUser)
    {
        IQueryable<OrderItem> query = dbContext.OrderItems;

        if (includeBook)
        {
            query = query.Include(oi => oi.Book);
        }

        if (includeUser)
        {
            query = query.Include(oi => oi.User);
        }

        return query;
    }
}