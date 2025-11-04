namespace WebAPI.Models.Response.Simplified;

public record ReviewSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    int Rating
) : BaseResponse(Id, CreatedAt);