using DataAccessLayer.Entities;

namespace WebAPI.Models.Response;

public record BookResponse(
    int Id,
    DateTime CreatedAt,
    string Title,
    string Description,
    decimal Price,
    List<int> AuthorIds,
    List<int> GenreIds,
    List<int> ReviewIds,
    int PublisherId
) : BaseResponse(Id, CreatedAt);
