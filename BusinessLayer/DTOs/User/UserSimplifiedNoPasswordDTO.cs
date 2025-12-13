namespace BusinessLayer.DTOs.User
{
    public class UserSimplifiedNoPasswordDTO : BaseDTO
    {
        public required string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
