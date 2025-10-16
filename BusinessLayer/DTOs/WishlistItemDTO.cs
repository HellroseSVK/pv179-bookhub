using BusinessLayer.DTOs.Simplified;

namespace BusinessLayer.DTOs;

public class WishlistItemDTO : BaseDTO
{
    public BookSimplifiedDTO Book { get; set; }
    public UserSimplifiedDTO User { get; set; }
}
