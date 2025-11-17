using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Models.Book;

public class BookWithImageModel
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public List<int> AuthorIds { get; set; } = [];
    public List<int> GenreIds { get; set; } = [];
    public int PublisherId { get; set; }

    [FromForm(Name = "image")]
    public IFormFile? Image { get; set; }
}