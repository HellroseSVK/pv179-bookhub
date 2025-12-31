namespace MVC.Models.User;

public class UserResetViewModel
{
    public string Email { get; set; } = string.Empty;
    public string ResetToken { get; set; } = string.Empty;
}