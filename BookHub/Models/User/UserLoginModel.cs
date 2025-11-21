using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.User;

public class UserLoginModel
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 8)]
    public required string Password { get; set; }
}