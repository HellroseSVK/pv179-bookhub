namespace BusinessLayer.DTOs.User
{
    internal class UserSimplifiedNoPassword : BaseDTO
    {
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
