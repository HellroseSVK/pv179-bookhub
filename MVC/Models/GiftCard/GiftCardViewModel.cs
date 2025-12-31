namespace MVC.Models.GiftCard;

public class GiftCardViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public decimal Price { get; set; }

    public bool IsCurrentlyValid =>
        DateTime.UtcNow >= ValidFrom && DateTime.UtcNow <= ValidTo;

    public List<GiftCodeViewModel> GiftCodes { get; set; } = [];
}
