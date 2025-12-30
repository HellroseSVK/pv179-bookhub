using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.GiftCard;

public class GiftCardUpsertDTO
{
    [Required]
    [StringLength(200)]
    public required string Title { get; set; }

    [Required]
    public required DateTime ValidFrom { get; set; }

    [Required]
    public required DateTime ValidTo { get; set; }

    [Required]
    [Range(0.01, 10000, ErrorMessage = "Price must be between 0.01 and 10000")]
    public decimal Price { get; set; }
}