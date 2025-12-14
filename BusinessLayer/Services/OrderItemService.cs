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
        var book = await FindBook(orderItemDto.BookId);
        var order = await FindOrder(orderItemDto.OrderId);

        var orderItem = orderItemDto.Adapt<OrderItem>();
        orderItem.BookId = book.Id;
        orderItem.OrderId = order.Id;

        var created = dbContext.OrderItems.Add(orderItem);
        await dbContext.SaveChangesAsync();

        return created.Entity.Adapt<OrderItemDTO>();
    }

    public async Task<OrderItemDTO> GetOrderItemAsync(int id, bool includeBook = true, bool includeOrder = true)
    {
        var orderItem = await FindOrderItem(id, includeBook, includeOrder);
        return orderItem.Adapt<OrderItemDTO>();
    }

    public async Task<List<OrderItemDTO>> GetOrderItemsAsync(int[]? ids = null, bool includeBook = true,
        bool includeOrder = true)
    {
        var query = GetBaseQuery(includeBook, includeOrder);

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

        if (orderItemDto.BookId != null)
        {
            var book = await FindBook((int)orderItemDto.BookId);
            orderItem.BookId = book.Id;
            orderItem.Book = book;
        }

        if (orderItemDto.OrderId != null)
        {
            var order = await FindOrder((int)orderItemDto.OrderId);
            orderItem.OrderId = order.Id;
            orderItem.Order = order;
        }

        dbContext.OrderItems.Update(orderItem);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateOrderItemAsync(int id, OrderItemUpsertDTO orderItemDto)
    {
        var orderItem = await FindOrderItem(id);

        var book = await FindBook(orderItemDto.BookId);
        var order = await FindOrder(orderItemDto.OrderId);

        orderItem.BookId = book.Id;
        orderItem.OrderId = order.Id;

        dbContext.OrderItems.Update(orderItem);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteOrderItemAsync(int id)
    {
        var orderItem = await FindOrderItem(id);

        dbContext.OrderItems.Remove(orderItem);
        await dbContext.SaveChangesAsync();
    }

    private async Task<OrderItem> FindOrderItem(int orderItemId, bool includeBook = false, bool includeOrder = false)
    {
        var query = GetBaseQuery(includeBook, includeOrder);
        var orderItem = await query.FirstOrDefaultAsync(oi => oi.Id == orderItemId);

        return orderItem ??
               throw new StatusCodeException(StatusCodes.Status404NotFound,
                   $"OrderItem with id {orderItemId} not found.");
    }

    private IQueryable<OrderItem> GetBaseQuery(bool includeBook, bool includeOrder)
    {
        IQueryable<OrderItem> query = dbContext.OrderItems;

        if (includeBook)
        {
            query = query.Include(oi => oi.Book);
        }

        if (includeOrder)
        {
            query = query.Include(oi => oi.Order);
        }

        return query;
    }

    private async Task<Book> FindBook(int bookId)
    {
        var book = await dbContext.Books
            .FirstOrDefaultAsync(book => bookId.Equals(book.Id));

        if (book == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Book with id {string.Join(", ", bookId)} not found.");
        }

        return book;
    }

    private async Task<Order> FindOrder(int orderId)
    {
        var order = await dbContext.Orders
            .FirstOrDefaultAsync(order => orderId.Equals(order.Id));

        if (order == null)
        {
            throw new StatusCodeException(StatusCodes.Status404NotFound,
                $"Order with id {string.Join(", ", orderId)} not found.");
        }

        return order;
    }
}