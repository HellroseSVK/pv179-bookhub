using WebAPI.Models.Book;
using WebAPI.Models.User;

namespace WebAPI.Models.Wishlist;

public record WishlistItemResponse(
    int Id,
    DateTime CreatedAt,
    BookSimplifiedResponse Book,
    UserSimplifiedResponse User
) : WishlistItemSimplifiedResponse(Id, CreatedAt);