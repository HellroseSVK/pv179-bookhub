using BusinessLayer.DTOs.Order;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services
{
    public class OrderService(CustomDBContext dbContext) : IOrderService
    {
        public async Task<OrderDTO> CreateOrderAsync(OrderUpsertDTO orderDto)
        {
            if (orderDto.BookIds == null || orderDto.BookIds.Count == 0)
            {
                throw new StatusCodeException(StatusCodes.Status400BadRequest,
                    "Order must contain at least one book.");
            }

            var books = await FindBooks(orderDto.BookIds);

            var order = new Order
            {
                UserId = orderDto.UserId,
                OrderItems = books.Select(b => new OrderItem { BookId = b.Id }).ToList()
            };

            var created = dbContext.Add(order);
            await dbContext.SaveChangesAsync();

            return created.Entity.Adapt<OrderDTO>();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await FindOrder(id);

            dbContext.Remove(order);
            await dbContext.SaveChangesAsync();
        }

        public async Task<OrderDTO> GetOrderAsync(int id, bool includeUser = true, bool includeBooks = true)
        {
            var order = await FindOrder(id, includeUser, includeBooks);

            return order.Adapt<OrderDTO>();
        }

        public async Task<List<OrderDTO>> GetOrdersAsync(int? userId, bool includeUser = true, bool includeBooks = true)
        {
            var query = GetBaseQuery(includeUser, includeBooks);

            if (userId != null)
            {
                query = query.Where(order => order.UserId == userId);
            }

            var orders = await query.ToListAsync();

            return orders.Adapt<List<OrderDTO>>();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task PatchOrderAsync(int id, OrderPartialDTO orderDto)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            throw new NotImplementedException();
        }

        private async Task<Order> FindOrder(int id, bool includeUser = true, bool includeBooks = true)
        {
            var query = GetBaseQuery(includeUser, includeBooks);
            var order = await query.FirstOrDefaultAsync(o => o.Id == id);

            return order ?? throw new StatusCodeException(StatusCodes.Status404NotFound, $"Order with id {id} not found.");
        }

        private async Task<List<Book>> FindBooks(List<int> ids)
        {
            var books = await dbContext.Books
                .Where(book => ids.Contains(book.Id))
                .ToListAsync();

            var missingBookIds = ids.Except(books.Select(book => book.Id)).ToList();
            if (missingBookIds.Any())
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound,
                    $"Books with ids {string.Join(", ", missingBookIds)} not found.");
            }

            return books;
        }

        private IQueryable<Order> GetBaseQuery(bool includeUser, bool includeBooks)
        {
            IQueryable<Order> query = dbContext.Orders;

            if (includeUser)
            {
                query = query.Include(o => o.User);
            }

            if (includeBooks)
            {
                query = query
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Book);
            }

            return query;
        }
    }
}
