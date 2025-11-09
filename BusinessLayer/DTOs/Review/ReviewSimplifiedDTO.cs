namespace BusinessLayer.DTOs.Review;

public class ReviewSimplifiedDTO : BaseDTO
{
    public int Rating { get; set; }
    public required string Body { get; set; }
}
