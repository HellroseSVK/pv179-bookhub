using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Order;

namespace BusinessLayer.DTOs.OrderItem;

public class OrderItemDTO : OrderItemSimplifiedDTO
{
    public BookSimplifiedDTO? Book { get; set; }
    public OrderSimplifiedDTO? Order { get; set; }
}