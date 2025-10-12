namespace WebAPI.Models.Response;

public record GenreResponse(
    int Id,
    DateTime CreatedAt,
    string Name,
    List<int> BookIds
) : BaseResponse(Id, CreatedAt);