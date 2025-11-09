namespace WebAPI.Models.Author;

public record AuthorSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    string Name,
    string Surname
) : BaseResponse(Id, CreatedAt);