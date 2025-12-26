namespace WebAPI.Models.Publisher;

public record PublisherSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    string Name
) : BaseResponse(Id, CreatedAt);