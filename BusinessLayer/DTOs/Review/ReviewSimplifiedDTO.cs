using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.Review;

public class ReviewSimplifiedDTO : BaseDTO
{
    [Range(1, 5)]
    public int Rating { get; set; }
    public required string Body { get; set; }
}
