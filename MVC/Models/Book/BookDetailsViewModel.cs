using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Review;

namespace MVC.Models.Book;

public class BookDetailsViewModel
{
    public BookViewModel Book { get; set; } = new BookViewModel();
    public List<ReviewSimplifiedDTO> Reviews { get; set; } = new List<ReviewSimplifiedDTO>();
}
