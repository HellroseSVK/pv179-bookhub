using WebAPI.Models.Response.Simplified;

namespace WebAPI.Models.Response;

public record OrderItemResponse(
    int Id,
    DateTime CreatedAt,
    BookSimplifiedResponse Book,
    UserSimplifiedResponse User
) : OrderItemSimplifiedResponse(Id, CreatedAt);