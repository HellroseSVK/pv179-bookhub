using BusinessLayer.DTOs.OrderItem;

namespace BusinessLayer.Services.Interfaces;

public interface IOrderItemService
{
    Task<OrderItemDTO> CreateOrderItemAsync(OrderItemUpsertDTO orderItem);
    Task<List<OrderItemDTO>> GetOrderItemsAsync(
        int[]? ids = null,
        bool includeBook = true,
        bool includeUser = true);
    Task<OrderItemDTO> GetOrderItemAsync(
        int id,
        bool includeBook = true,
        bool includeUser = true);
    Task PatchOrderItemAsync(int id, OrderItemPartialDTO orderItem);
    Task UpdateOrderItemAsync(int id, OrderItemUpsertDTO orderItem);
    Task DeleteOrderItemAsync(int id);
}