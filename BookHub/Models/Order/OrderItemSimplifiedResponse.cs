namespace WebAPI.Models.Order;

public record OrderItemSimplifiedResponse(
    int Id,
    DateTime CreatedAt
) : BaseResponse(Id, CreatedAt);