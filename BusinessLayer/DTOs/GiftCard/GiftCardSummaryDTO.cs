namespace BusinessLayer.DTOs.GiftCard;

public class GiftCardSummaryDTO : BaseDTO
{
    public required string Title { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public decimal Price { get; set; }
}
