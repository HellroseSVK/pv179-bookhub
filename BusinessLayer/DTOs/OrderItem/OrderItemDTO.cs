using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.User;

namespace BusinessLayer.DTOs.OrderItem;

public class OrderItemDTO
{
    public BookSimplifiedDTO Book { get; set; }
    public UserSimplifiedDTO User { get; set; }
}
