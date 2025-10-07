namespace WebAPI.Models
{
    public class UserPartialModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public bool? IsAdmin { get; set; }

        public List<int>? ReviewIds { get; set; }

        public List<int>? WishlistItemIds { get; set; }

        public List<int>? OrderItemIds { get; set; }
    }
}
