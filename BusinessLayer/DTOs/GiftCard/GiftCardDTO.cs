namespace BusinessLayer.DTOs.GiftCard;

public class GiftCardDTO : BaseDTO
{
    public required string Title { get; set; }
    public List<GiftCodeDTO> GiftCodes { get; set; } = [];
}