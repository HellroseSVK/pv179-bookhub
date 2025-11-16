using BusinessLayer.DTOs.OrderItem;

namespace BusinessLayer.Services.Interfaces;

public interface IOrderItemService
{
    Task<OrderItemDTO> CreateOrderItemAsync(OrderItemUpsertDTO orderItemDto);
    Task<List<OrderItemDTO>> GetOrderItemsAsync(
        int[]? ids = null,
        bool includeBook = true,
        bool includeUser = true);
    Task<OrderItemDTO> GetOrderItemAsync(
        int id,
        bool includeBook = true,
        bool includeUser = true);
    Task PatchOrderItemAsync(int id, OrderItemPartialDTO orderItemDto);
    Task UpdateOrderItemAsync(int id, OrderItemUpsertDTO orderItemDto);
    Task DeleteOrderItemAsync(int id);
}