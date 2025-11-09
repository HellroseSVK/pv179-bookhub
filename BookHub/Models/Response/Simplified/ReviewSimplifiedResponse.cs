namespace WebAPI.Models.Response.Simplified;

public record ReviewSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    int Rating,
    string Body,
    BookSimplifiedResponse? Book,
    UserSimplifiedResponse? User
) : BaseResponse(Id, CreatedAt);