using BusinessLayer.DTOs.Simplified;

namespace BusinessLayer.DTOs;

public class UserDTO : UserSimplifiedDTO
{
    public List<ReviewSimplifiedDTO> Reviews { get; set; }
    public List<WishlistItemDTO> WishlistItems { get; set; }
    public List<OrderItemDTO> OrderItems { get; set; }
};