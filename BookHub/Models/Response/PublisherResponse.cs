using WebAPI.Models.Response.Simplified;

namespace WebAPI.Models.Response;

public record PublisherResponse(
    int Id,
    DateTime CreatedAt,
    string Name,
    List<BookSimplifiedResponse> Books
) : PublisherSimplifiedResponse(Id, CreatedAt, Name);