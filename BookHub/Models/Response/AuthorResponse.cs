using WebAPI.Models.Response.Simplified;

namespace WebAPI.Models.Response;

public record AuthorResponse(
    int Id,
    DateTime CreatedAt,
    string Name,
    string Surname,
    List<BookSimplifiedResponse> Books
) : AuthorSimplifiedResponse(Id, CreatedAt, Name, Surname);