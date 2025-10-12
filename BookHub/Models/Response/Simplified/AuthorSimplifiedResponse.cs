namespace WebAPI.Models.Response.Simplified;

public record AuthorSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    string Name,
    string Surname
) : BaseResponse(Id, CreatedAt);