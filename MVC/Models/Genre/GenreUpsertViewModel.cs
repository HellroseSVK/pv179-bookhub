using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Models.Genre;

public class GenreUpsertViewModel
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Books")]
    public List<int> SelectedBooksIds { get; set; } = [];
    public List<SelectListItem> Books { get; set; } = [];
}
