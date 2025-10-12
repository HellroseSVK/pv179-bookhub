using WebAPI.Models.Response.Simplified;

namespace WebAPI.Models.Response;

public record UserResponse(
    int Id,
    DateTime CreatedAt,
    string Email,
    string Password,
    bool IsAdmin,
    List<ReviewSimplifiedResponse> Reviews,
    List<WishlistItemSimplifiedResponse> WishlistItems,
    List<OrderItemSimplifiedResponse> OrderItems
) : UserSimplifiedResponse(Id, CreatedAt, Email, Password, IsAdmin);