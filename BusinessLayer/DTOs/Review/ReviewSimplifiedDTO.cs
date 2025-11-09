using System.ComponentModel.DataAnnotations;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.User;

namespace BusinessLayer.DTOs.Review;

public class ReviewSimplifiedDTO : BaseDTO
{
    [Range(1, 5)]
    public int Rating { get; set; }
    public string Body { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
    public BookSimplifiedDTO? Book { get; set; }
    public UserSimplifiedDTO? User { get; set; }
}
