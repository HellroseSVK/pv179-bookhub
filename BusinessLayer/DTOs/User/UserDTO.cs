using BusinessLayer.DTOs.Order;
using BusinessLayer.DTOs.Review;
using BusinessLayer.DTOs.WishlistItem;

namespace BusinessLayer.DTOs.User;

public class UserDTO : UserSimplifiedDTO
{
    public List<ReviewSimplifiedDTO> Reviews { get; set; } = [];
    public List<WishlistItemDTO> WishlistItems { get; set; } = [];
    public List<OrderDTO> Orders { get; set; } = [];
};