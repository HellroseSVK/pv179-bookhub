using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Review;

public class ReviewModel
{
    [Required]
    [Range(0, 100)]
    public int Rating { get; set; }

    [Required]
    [StringLength(500, MinimumLength = 1)]
    public required string Body { get; set; }

    [Required]
    public int BookId { get; set; }

    [Required]
    public int UserId { get; set; }
}