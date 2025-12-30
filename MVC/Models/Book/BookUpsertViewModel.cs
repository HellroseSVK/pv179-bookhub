using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Book;

public class BookUpsertViewModel
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 999999.99)]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Publisher is required")]
    [Display(Name = "Publisher")]
    public int SelectedPublisherId { get; set; }

    [Required(ErrorMessage = "Primary genre is required")]
    [Display(Name = "Primary Genre")]
    public int SelectedPrimaryGenreId { get; set; }

    [Display(Name = "Secondary Genres")]
    public List<int> SelectedSecondaryGenreIds { get; set; } = [];

    [Display(Name = "Authors")]
    public List<int> SelectedAuthorIds { get; set; } = [];

    [Display(Name = "Book Cover Image")]
    public IFormFile? ImageFile { get; set; }

    public string? CurrentImageUrl { get; set; }

    public List<SelectListItem> Publishers { get; set; } = [];
    public List<SelectListItem> Genres { get; set; } = [];
    public List<SelectListItem> Authors { get; set; } = [];
}
