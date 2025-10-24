namespace BusinessLayer.DTOs.User
{
    internal class UserSimplifiedNoPasswordDTO : BaseDTO
    {
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
