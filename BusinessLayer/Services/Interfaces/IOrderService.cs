using BusinessLayer.DTOs.Order;

namespace BusinessLayer.Services.Interfaces;

public interface IOrderService
{
    Task<OrderDTO> CreateOrderAsync(OrderUpsertDTO order);
    Task<OrderDTO> GetOrderAsync(int id, bool includeUser = true, bool includeBooks = true);
    Task<List<OrderDTO>> GetOrdersAsync(int? userId, bool includeUser = true, bool includeBooks = true);
    Task PatchOrderAsync(int id, OrderPartialDTO order);
    Task DeleteOrderAsync(int id);

    // Cart methods
    Task<OrderDTO> GetUserCartAsync(int userId);
    Task<OrderDTO> AddBookToCartAsync(int userId, int bookId);
    Task<OrderDTO> RemoveItemFromCartAsync(int userId, int orderItemId);
    Task<OrderDTO> ApplyGiftCodeToCartAsync(int userId, string code);
    Task<OrderDTO> RemoveGiftCodeFromCartAsync(int userId);
    Task<CartSummaryDTO> CalculateCartTotalsAsync(int userId);
    Task<OrderDTO> CompleteUserCartAsync(int userId);
}
