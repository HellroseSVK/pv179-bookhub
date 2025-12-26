namespace WebAPI.Models.Wishlist;

public record WishlistItemSimplifiedResponse(
    int Id,
    DateTime CreatedAt
) : BaseResponse(Id, CreatedAt);