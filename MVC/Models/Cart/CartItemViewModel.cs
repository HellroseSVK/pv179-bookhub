namespace MVC.Models.Cart;

public class CartItemViewModel
{
    public int OrderItemId { get; set; }
    public int BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Author { get; set; }
    public string? CoverImageUrl { get; set; }
    public decimal Price { get; set; }
}
