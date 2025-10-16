namespace BusinessLayer.DTOs.Simplified;

public class UserSimplifiedDTO : BaseDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
};