using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Author;

public class AuthorPartialModel
{
    [StringLength(50, MinimumLength = 1)]
    public string? Name { get; set; }

    [StringLength(50, MinimumLength = 1)]
    public string? Surname { get; set; }

    public List<int>? BookIds { get; set; }
}