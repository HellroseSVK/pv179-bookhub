namespace WebAPI.Models.Response;

public record AuthorResponse(
    int Id,
    DateTime CreatedAt,
    string Name,
    string Surname,
    List<int> BookIds
) : BaseResponse(Id, CreatedAt);
