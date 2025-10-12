namespace WebAPI.Models.Response.Simplified;

public record WishlistItemSimplifiedResponse(
    int Id,
    DateTime CreatedAt
) : BaseResponse(Id, CreatedAt);