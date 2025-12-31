using BusinessLayer.DTOs.Order;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Exception;

namespace BusinessLayer.Services
{
    public class OrderService(
        CustomDBContext dbContext,
        IBookService bookService,
        IGiftCardService giftCardService) : IOrderService
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

        // Cart methods

        public async Task<OrderDTO> GetUserCartAsync(int userId)
        {
            var cart = await dbContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Book)
                .Include(o => o.GiftCode)
                    .ThenInclude(gc => gc!.GiftCard)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderStatus == OrderStatus.Draft);

            if (cart == null)
            {
                cart = new Order
                {
                    UserId = userId,
                    OrderStatus = OrderStatus.Draft
                };

                dbContext.Orders.Add(cart);
                await dbContext.SaveChangesAsync();
            }

            return cart.Adapt<OrderDTO>();
        }

        public async Task<OrderDTO> AddBookToCartAsync(int userId, int bookId)
        {
            var cart = await GetUserCartAsync(userId);

            // Verify book exists
            await bookService.GetBookAsync(bookId);

            // Check if book already in cart
            var cartEntity = await dbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == cart.Id);

            if (cartEntity!.OrderItems.Any(oi => oi.BookId == bookId))
            {
                // Book already in cart, return current cart
                return cart;
            }

            // Add new order item
            var orderItem = new OrderItem
            {
                OrderId = cart.Id,
                BookId = bookId
            };

            dbContext.OrderItems.Add(orderItem);
            await dbContext.SaveChangesAsync();

            // Reload and return updated cart
            return await GetUserCartAsync(userId);
        }

        public async Task<OrderDTO> RemoveItemFromCartAsync(int userId, int orderItemId)
        {
            var cart = await GetUserCartAsync(userId);

            var orderItem = await dbContext.OrderItems
                .FirstOrDefaultAsync(oi => oi.Id == orderItemId && oi.OrderId == cart.Id);

            if (orderItem == null)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound,
                    "Order item not found in cart.");
            }

            dbContext.OrderItems.Remove(orderItem);
            await dbContext.SaveChangesAsync();

            // Reload and return updated cart
            return await GetUserCartAsync(userId);
        }

        public async Task<OrderDTO> ApplyGiftCodeToCartAsync(int userId, string code)
        {
            // Validate gift code
            var giftCodeDto = await giftCardService.ValidateGiftCodeAsync(code);

            var cart = await GetUserCartAsync(userId);

            // Get cart entity
            var cartEntity = await dbContext.Orders
                .Include(o => o.GiftCode)
                .FirstOrDefaultAsync(o => o.Id == cart.Id);

            if (cartEntity!.GiftCode != null)
            {
                throw new StatusCodeException(StatusCodes.Status400BadRequest,
                    "Cart already has a gift code applied. Remove it first.");
            }

            // Apply gift code
            // Need to fetch full GiftCode entity to update it
            var giftCodeEntity = await dbContext.GiftCodes
                .FirstOrDefaultAsync(gc => gc.Id == giftCodeDto.Id);

            if (giftCodeEntity == null) throw new StatusCodeException(StatusCodes.Status404NotFound, "Gift code not found.");

            giftCodeEntity.OrderId = cart.Id;
            dbContext.GiftCodes.Update(giftCodeEntity);
            await dbContext.SaveChangesAsync();

            // Reload and return updated cart
            return await GetUserCartAsync(userId);
        }

        public async Task<OrderDTO> RemoveGiftCodeFromCartAsync(int userId)
        {
            var cart = await GetUserCartAsync(userId);

            var cartEntity = await dbContext.Orders
                .Include(o => o.GiftCode)
                .FirstOrDefaultAsync(o => o.Id == cart.Id);

            if (cartEntity!.GiftCode == null)
            {
                // No gift code to remove, return cart as-is
                return cart;
            }

            var giftCode = cartEntity.GiftCode;
            giftCode.OrderId = null;
            dbContext.GiftCodes.Update(giftCode);
            await dbContext.SaveChangesAsync();

            // Reload and return updated cart
            return await GetUserCartAsync(userId);
        }

        public async Task<CartSummaryDTO> CalculateCartTotalsAsync(int userId)
        {
            var cart = await dbContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Book)
                .Include(o => o.GiftCode)
                    .ThenInclude(gc => gc!.GiftCard)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderStatus == OrderStatus.Draft);

            if (cart == null)
            {
                return new CartSummaryDTO
                {
                    Subtotal = 0,
                    Discount = 0,
                    Total = 0,
                    AppliedGiftCode = null
                };
            }

            var subtotal = cart.OrderItems.Sum(oi => oi.Book!.Price);
            var discount = cart.GiftCode?.GiftCard?.Price ?? 0;
            var total = Math.Max(0, subtotal - discount);

            return new CartSummaryDTO
            {
                Subtotal = subtotal,
                Discount = discount,
                Total = total,
                AppliedGiftCode = cart.GiftCode?.Code
            };
        }

        public async Task<OrderDTO> CompleteUserCartAsync(int userId)
        {
            var cart = await dbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.OrderStatus == OrderStatus.Draft);

            if (cart == null)
            {
                throw new StatusCodeException(StatusCodes.Status404NotFound, "No active cart found to complete.");
            }

            if (!cart.OrderItems.Any())
            {
                throw new StatusCodeException(StatusCodes.Status400BadRequest, "Cannot checkout an empty cart.");
            }

            cart.OrderStatus = OrderStatus.Completed;
            dbContext.Orders.Update(cart);
            await dbContext.SaveChangesAsync();

            return cart.Adapt<OrderDTO>();
        }
    }
}
