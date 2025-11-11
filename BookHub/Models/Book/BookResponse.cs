using WebAPI.Models.Author;
using WebAPI.Models.Genre;
using WebAPI.Models.Publisher;
using WebAPI.Models.Review;

namespace WebAPI.Models.Book;

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
