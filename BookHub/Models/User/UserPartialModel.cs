using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.User;

public class UserPartialModel
{
    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(100, MinimumLength = 8)]
    public string? Password { get; set; }

    public bool? IsAdmin { get; set; }
}