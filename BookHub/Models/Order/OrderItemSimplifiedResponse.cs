using WebAPI.Models.Book;
using WebAPI.Models.User;

namespace WebAPI.Models.Order;

public record OrderItemSimplifiedResponse(
    int Id,
    DateTime CreatedAt
) : BaseResponse(Id, CreatedAt);