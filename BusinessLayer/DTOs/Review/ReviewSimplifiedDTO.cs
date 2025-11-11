using System.ComponentModel.DataAnnotations;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.User;

namespace BusinessLayer.DTOs.Review;

public class ReviewSimplifiedDTO : BaseDTO
{
    [Range(1, 5)]
    public int Rating { get; set; }
    public required string Body { get; set; }
}
