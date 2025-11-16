namespace BusinessLayer.DTOs.User;

public class UserUpsertDTO
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool IsAdmin { get; set; }
}