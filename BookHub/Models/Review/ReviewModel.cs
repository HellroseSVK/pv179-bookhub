using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Review;

public class ReviewModel
{
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 1)]
    public required string Body { get; set; }

    [Required]
    public int BookId { get; set; }

    [Required]
    public int UserId { get; set; }
}