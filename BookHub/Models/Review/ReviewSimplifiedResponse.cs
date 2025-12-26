namespace WebAPI.Models.Review;

public record ReviewSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    int Rating,
    string Body
) : BaseResponse(Id, CreatedAt);