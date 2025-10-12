namespace WebAPI.Models.Response.Simplified;

public record PublisherSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    string Name
) : BaseResponse(Id, CreatedAt);