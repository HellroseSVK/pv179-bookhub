using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Genre;

public class GenreModel
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public required string Name { get; set; }

    [Required]
    public List<int> BookIds { get; set; } = [];
}