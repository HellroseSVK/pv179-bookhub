using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.User;

namespace BusinessLayer.DTOs.WishlistItem;

public class WishlistItemDTO : BaseDTO
{
    public BookSimplifiedDTO? Book { get; set; }
    public UserSimplifiedDTO? User { get; set; }
}
