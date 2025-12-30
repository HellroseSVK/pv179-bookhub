namespace BusinessLayer.DTOs.Order;

public class CartSummaryDTO
{
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public string? AppliedGiftCode { get; set; }
}
