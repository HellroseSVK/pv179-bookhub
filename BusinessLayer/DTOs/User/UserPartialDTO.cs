namespace BusinessLayer.DTOs.User
{
    public class UserPartialDTO
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool? IsAdmin { get; set; }
    }
}
