using WebAPI.Models.Book;

namespace WebAPI.Models.Genre;

public record GenreResponse(
    int Id,
    DateTime CreatedAt,
    string Name,
    List<BookSimplifiedResponse> Books
) : GenreSimplifiedResponse(Id, CreatedAt, Name);