namespace WebAPI.Models.Book;

public record BookSimplifiedResponse(
    int Id,
    DateTime CreatedAt,
    string Title,
    string Description,
    decimal Price
) : BaseResponse(Id, CreatedAt);