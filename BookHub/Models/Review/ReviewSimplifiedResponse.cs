namespace WebAPI.Models.Review;

public record ReviewSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    int Rating
) : BaseResponse(Id, CreatedAt);