namespace MVC.Models.User;

public class UserSimplifiedViewModel
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false;
}
