namespace BusinessLayer.DTOs.User
{
    internal class UserSimplifiedNoPasswordDTO : BaseDTO
    {
        public required string Email { get; set; }
        public required bool IsAdmin { get; set; }
    }
}
