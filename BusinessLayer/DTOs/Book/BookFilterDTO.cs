namespace BusinessLayer.DTOs.Book;

public class BookFilterDTO : PaginationParameters
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? PriceMin { get; set; }
    public decimal? PriceMax { get; set; }
    public decimal? RatingMin { get; set; }
    public List<int>? AuthorIds { get; set; }
    public List<int>? GenreIds { get; set; }
    public List<int>? PublisherIds { get; set; }

    public string? SearchTerm { get; set; }
}