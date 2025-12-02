using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Review;

public class ReviewPartialModel
{
    [Range(1, 5)]
    public int? Rating { get; set; }

    [StringLength(500, MinimumLength = 1)]
    public string? Body { get; set; }

    public int? BookId { get; set; }

    public int? UserId { get; set; }
}