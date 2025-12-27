using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Genre;

public class GenrePartialModel
{
    [StringLength(50, MinimumLength = 1)]
    public string? Name { get; set; }

    public List<int>? BookIds { get; set; }
}