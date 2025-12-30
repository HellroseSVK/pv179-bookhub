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
}