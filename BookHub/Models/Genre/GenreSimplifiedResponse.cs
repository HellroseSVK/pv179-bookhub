namespace WebAPI.Models.Genre;

public record GenreSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    string Name
) : BaseResponse(Id, CreatedAt);