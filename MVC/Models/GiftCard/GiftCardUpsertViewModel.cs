using System.ComponentModel.DataAnnotations;

namespace MVC.Models.GiftCard;

public class GiftCardUpsertViewModel
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Valid From date is required")]
    [Display(Name = "Valid From")]
    public DateTime ValidFrom { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Valid To date is required")]
    [Display(Name = "Valid To")]
    public DateTime ValidTo { get; set; } = DateTime.Today.AddYears(1);
}
