using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.Review;

public class ReviewPartialDTO
{
    [Range(1, 5)]
    public int? Rating { get; set; }
    
    public string? Body { get; set; }
    
    public int? BookId { get; set; }
    
    public int? UserId { get; set; }
}