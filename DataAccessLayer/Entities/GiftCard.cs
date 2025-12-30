namespace DataAccessLayer.Entities;

public class GiftCard : BaseEntity
{
    public required string Title { get; set; }
    public required DateTime ValidFrom { get; set; }
    public required DateTime ValidTo { get; set; }
    public decimal Price { get; set; }

    public List<GiftCode> GiftCodes { get; set; } = [];
}