using WebAPI.Models.Response.Simplified;

namespace WebAPI.Models.Response;

public record GenreResponse(
    int Id,
    DateTime CreatedAt,
    string Name,
    List<BookSimplifiedResponse> Books
) : GenreSimplifiedResponse(Id, CreatedAt, Name);