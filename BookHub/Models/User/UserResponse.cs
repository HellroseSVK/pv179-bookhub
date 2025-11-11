using WebAPI.Models.Order;
using WebAPI.Models.Review;
using WebAPI.Models.Wishlist;

namespace WebAPI.Models.User;

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