namespace WebAPI.Models.User
{
    public class UserPartialModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool? IsAdmin { get; set; }
    }
}
