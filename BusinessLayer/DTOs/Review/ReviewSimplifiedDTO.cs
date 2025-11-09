namespace BusinessLayer.DTOs.Review;

public class ReviewSimplifiedDTO : BaseDTO
{
    public int Rating { get; set; }
    public string Body { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
}
