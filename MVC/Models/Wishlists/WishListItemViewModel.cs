using MVC.Models.Book;

namespace MVC.Models.Wishlists;

public class WishListItemViewModel
{
    public int Id { get; set; }
    public required BookSimplifiedViewModel Book { get; set; }
}