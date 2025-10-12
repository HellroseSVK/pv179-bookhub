using WebAPI.Models.Response.Simplified;

namespace WebAPI.Models.Response;

public record WishlistItemResponse(
    int Id,
    DateTime CreatedAt,
    BookSimplifiedResponse Book,
    UserSimplifiedResponse User
) : WishlistItemSimplifiedResponse(Id, CreatedAt);