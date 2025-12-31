namespace MVC.Models.GiftCard;

public class GiftCodeSimplifiedViewModel
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int? OrderId { get; set; }

    public bool IsUsed => OrderId.HasValue;
}
