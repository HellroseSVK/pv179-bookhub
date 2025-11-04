namespace WebAPI.Models.Response.Simplified;

public record OrderItemSimplifiedResponse(
    int Id,
    DateTime CreatedAt
) : BaseResponse(Id, CreatedAt);