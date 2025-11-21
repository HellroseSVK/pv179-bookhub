using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Author;

public class AuthorModel
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public required string Name { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public required string Surname { get; set; }

    [Required]
    public List<int> BookIds { get; set; } = [];
}