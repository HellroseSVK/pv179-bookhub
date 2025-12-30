using System.ComponentModel.DataAnnotations;

namespace MVC.Models.GiftCard;

public class GiftCodesUpsertViewModel
{
    public int GiftCardId { get; set; }

    [Required]
    [Range(0, 1000)]
    public required int Count { get; set; }
}
