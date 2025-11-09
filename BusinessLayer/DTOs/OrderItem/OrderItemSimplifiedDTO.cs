using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.User;

namespace BusinessLayer.DTOs.OrderItem;

public class OrderItemSimplifiedDTO : BaseDTO
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    public BookSimplifiedDTO? Book { get; set; }
    public UserSimplifiedDTO? User { get; set; }
}