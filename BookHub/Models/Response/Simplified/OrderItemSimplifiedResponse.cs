namespace WebAPI.Models.Response.Simplified;

public record OrderItemSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    BookSimplifiedResponse? Book,
    UserSimplifiedResponse? User
) : BaseResponse(Id, CreatedAt);