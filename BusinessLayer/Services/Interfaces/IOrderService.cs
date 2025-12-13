using BusinessLayer.DTOs.Order;

namespace BusinessLayer.Services.Interfaces;

public interface IOrderService
{
    Task<OrderDTO> CreateOrderAsync(OrderUpsertDTO order);
    Task<OrderDTO> GetOrderAsync(int id, bool includeUser = true, bool includeBooks = true);
    Task<List<OrderDTO>> GetOrdersAsync(int? userId, bool includeUser = true, bool includeBooks = true);
    Task PatchOrderAsync(int id, OrderPartialDTO order);
    Task DeleteOrderAsync(int id);
}
