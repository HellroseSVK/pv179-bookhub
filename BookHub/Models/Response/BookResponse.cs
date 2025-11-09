using WebAPI.Models.Response.Simplified;

namespace WebAPI.Models.Response;

public record BookResponse(
    int Id,
    DateTime CreatedAt,
    string Title,
    string Description,
    decimal Price,
    List<AuthorSimplifiedResponse> Authors,
    List<GenreSimplifiedResponse> Genres,
    List<ReviewSimplifiedResponse> Reviews,
    PublisherSimplifiedResponse Publisher
) : BookSimplifiedResponse(Id, CreatedAt, Title, Description, Price);
