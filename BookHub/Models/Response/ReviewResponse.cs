using WebAPI.Models.Response.Simplified;

namespace WebAPI.Models.Response;

public record ReviewResponse(
    int Id,
    DateTime CreatedAt,
    int Rating,
    BookSimplifiedResponse Book,
    UserSimplifiedResponse User
) : ReviewSimplifiedResponse(Id, CreatedAt, Rating);