namespace DataAccessLayer.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public List<Review> Reviews { get; set; }

        public List<WishlistItem> WishlistItems { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
