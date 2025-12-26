using WebAPI.Models.Book;

namespace WebAPI.Models.Publisher;

public record PublisherResponse(
    int Id,
    DateTime CreatedAt,
    string Name,
    List<BookSimplifiedResponse> Books
) : PublisherSimplifiedResponse(Id, CreatedAt, Name);