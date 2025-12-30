namespace DataAccessLayer.Entities;

public class GiftCode : BaseEntity
{
    public required string Code { get; set; }

    public int GiftCardId { get; set; }

    public GiftCard? GiftCard { get; set; }

    public int? OrderId { get; set; }

    public Order? Order { get; set; }
}