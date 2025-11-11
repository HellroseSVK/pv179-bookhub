namespace WebAPI.Models.Review
{
    public class ReviewPartialModel
    {
        public int? Rating { get; set; }

        public string? Body { get; set; }

        public int? BookId { get; set; }

        public int? UserId { get; set; }
    }
}
