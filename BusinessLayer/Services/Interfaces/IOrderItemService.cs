using BusinessLayer.DTOs.OrderItem;

namespace BusinessLayer.Services.Interfaces;

public interface IOrderItemService
{
    Task<OrderItemSimplifiedDTO> CreateOrderItemAsync(OrderItemCreateDTO orderItem);
    Task<List<OrderItemSimplifiedDTO>> GetOrderItemsAsync(
        int[]? ids = null,
        bool includeBook = true,
        bool includeUser = true);
    Task<OrderItemSimplifiedDTO> GetOrderItemAsync(
        int id,
        bool includeBook = true,
        bool includeUser = true);
    Task PatchOrderItemAsync(int id, OrderItemPartialDTO orderItem);
    Task UpdateOrderItemAsync(int id, OrderItemUpdateDTO orderItem);
    Task DeleteOrderItemAsync(int id);
}