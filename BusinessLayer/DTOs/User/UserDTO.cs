using BusinessLayer.DTOs.OrderItem;
using BusinessLayer.DTOs.Review;
using BusinessLayer.DTOs.WishlistItem;

namespace BusinessLayer.DTOs.User;

public class UserDTO : UserSimplifiedDTO
{
    public List<ReviewSimplifiedDTO> Reviews { get; set; } = [];
    public List<WishlistItemDTO> WishlistItems { get; set; } = [];
    public List<OrderItemDTO> OrderItems { get; set; } = [];
};