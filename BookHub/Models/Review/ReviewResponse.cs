using WebAPI.Models.Book;
using WebAPI.Models.User;

namespace WebAPI.Models.Review;

public record ReviewResponse(
    int Id,
    DateTime CreatedAt,
    int Rating,
    string Body,
    BookSimplifiedResponse Book,
    UserSimplifiedResponse User
) : ReviewSimplifiedResponse(Id, CreatedAt, Rating, Body);