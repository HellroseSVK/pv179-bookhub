namespace WebAPI.Models.Response;

public record PublisherResponse(
    int Id,
    DateTime CreatedAt,
    string Name,
    List<int> BookIds
    ) : BaseResponse(Id, CreatedAt);
    