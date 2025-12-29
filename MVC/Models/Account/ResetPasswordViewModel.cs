using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Account;

public class ResetPasswordViewModel
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public required string ConfirmPassword { get; set; }

    [Required]
    [DataType(DataType.Text)]
    public required string Token { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}