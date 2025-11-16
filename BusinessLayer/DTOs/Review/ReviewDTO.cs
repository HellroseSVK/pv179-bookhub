using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.User;

namespace BusinessLayer.DTOs.Review;

public class ReviewDTO : ReviewSimplifiedDTO
{
    public BookSimplifiedDTO? Book { get; set; }
    public UserSimplifiedDTO? User { get; set; }
}
