using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Wishlist;

public class WishlistItemModel
{
    [Required]
    public int BookId { get; set; }

    [Required]
    public int UserId { get; set; }
}