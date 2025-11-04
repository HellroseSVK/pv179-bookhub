namespace WebAPI.Models.Response.Simplified;

public record BookSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    string Title,
    string Description,
    decimal Price
) : BaseResponse(Id, CreatedAt);