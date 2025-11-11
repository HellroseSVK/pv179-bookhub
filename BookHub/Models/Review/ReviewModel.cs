namespace WebAPI.Models.Review
{
    public class ReviewModel
    {
        public int Rating { get; set; }

        public required string Body { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }
    }
}
