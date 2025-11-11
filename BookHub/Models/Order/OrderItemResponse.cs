using WebAPI.Models.Book;
using WebAPI.Models.User;

namespace WebAPI.Models.Order;

public record OrderItemResponse(
    int Id,
    DateTime CreatedAt,
    BookSimplifiedResponse Book,
    UserSimplifiedResponse User
) : OrderItemSimplifiedResponse(Id, CreatedAt);