using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Order;

namespace BusinessLayer.Services.Interfaces;

public interface IOrderService
{
    Task<OrderDTO> CreateOrderAsync(OrderUpsertDTO order);
    Task<OrderDTO> GetOrderAsync(int id, bool includeUser = true, bool includeBooks = true);
    Task<List<OrderDTO>> GetOrdersAsync(int? userId, bool includeUser = true, bool includeBooks = true, bool includeGiftCode = true);

    public Task<PagedResult<OrderDTO>> GetOrdersPaginatedAsync(PaginationParameters paginationParameters,
        int? userId = null, bool includeUser = true, bool includeBooks = true, bool includeGiftCode = true);

    Task UpdateOrderAsync(int id, OrderUpsertDTO order);
    Task PatchOrderAsync(int id, OrderPartialDTO order);
    Task DeleteOrderAsync(int id);

    // Cart methods
    Task<OrderDTO> GetUserCartAsync(int userId);
    Task<OrderDTO> AddBookToCartAsync(int userId, int bookId);
    Task<OrderDTO> RemoveItemFromCartAsync(int userId, int orderItemId);
    Task<OrderDTO> ApplyGiftCodeToOrderAsync(int userId, string code, int? orderId = null);
    Task<OrderDTO> RemoveGiftCodeFromCartAsync(int userId);
    Task<CartSummaryDTO> CalculateCartTotalsAsync(int userId);
    Task<OrderDTO> CompleteUserCartAsync(int userId);
}