namespace BusinessLayer.DTOs.Book;

public class BookUpdateDTO
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public int PublisherId { get; set; }
    public List<int> AuthorIds { get; set; } = [];
    public List<int> GenreIds { get; set; } = [];
}