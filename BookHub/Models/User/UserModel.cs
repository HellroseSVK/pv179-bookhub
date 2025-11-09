namespace WebAPI.Models.User
{
    public class UserModel
    {
        public required string Email { get; set; }

        public required string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
