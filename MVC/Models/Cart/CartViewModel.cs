namespace MVC.Models.Cart;

public class CartViewModel
{
    public int CartId { get; set; }
    public List<CartItemViewModel> Items { get; set; } = new();
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string? AppliedGiftCode { get; set; }
    public bool HasItems => Items.Any();
}
