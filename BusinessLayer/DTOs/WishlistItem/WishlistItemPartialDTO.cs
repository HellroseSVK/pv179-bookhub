namespace BusinessLayer.DTOs.WishlistItem
{
    public class WishlistItemPartialDTO : BaseDTO
    {
        public int? BookId { get; set; }
        public int? UserId { get; set; }
    }
}
