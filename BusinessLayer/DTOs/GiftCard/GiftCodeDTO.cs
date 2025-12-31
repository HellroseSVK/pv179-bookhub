namespace BusinessLayer.DTOs.GiftCard;

public class GiftCodeDTO : BaseDTO
{
    public required string Code { get; set; }
    public int? OrderId { get; set; }
}