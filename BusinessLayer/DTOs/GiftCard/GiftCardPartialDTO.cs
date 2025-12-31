using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.GiftCard;

public class GiftCardPartialDTO
{
    public string? Title { get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }

    [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000")]
    public decimal? Price { get; set; }
}