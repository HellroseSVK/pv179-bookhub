using WebAPI.Models.Book;

namespace WebAPI.Models.Author;

public record AuthorResponse(
    int Id,
    DateTime CreatedAt,
    string Name,
    string Surname,
    List<BookSimplifiedResponse> Books
) : AuthorSimplifiedResponse(Id, CreatedAt, Name, Surname);