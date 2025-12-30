namespace MVC.Models.GiftCard;

public class GiftCardViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;

    public List<GiftCodeSimplifiedViewModel> GiftCodes { get; set; } = [];
}
