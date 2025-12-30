namespace DataAccessLayer.Entities;

public class GiftCard : BaseEntity
{
    public required string Title { get; set; }

    public List<GiftCode> GiftCodes { get; set; } = [];
}