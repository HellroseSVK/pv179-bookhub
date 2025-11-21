using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Book;

public class BookFilter
{
    [StringLength(50, MinimumLength = 1)]
    public string? Title { get; set; }

    [StringLength(1000, MinimumLength = 1)]
    public string? Description { get; set; }

    [Range(0.00, double.MaxValue)]
    public decimal? PriceMin { get; set; }

    [Range(0.00, double.MaxValue)]
    public decimal? PriceMax { get; set; }

    [Range(0.00, 100.00)]
    public decimal? RatingMin { get; set; }

    public List<int>? AuthorIds { get; set; }
    public List<int>? GenreIds { get; set; }
    public List<int>? PublisherIds { get; set; }
}