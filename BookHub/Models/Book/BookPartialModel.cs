using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Book;

public class BookPartialModel
{
    [StringLength(50, MinimumLength = 1)]
    public string? Title { get; set; }

    [StringLength(1000, MinimumLength = 1)]
    public string? Description { get; set; }

    [Range(0.00, double.MaxValue)]
    public decimal? Price { get; set; }

    public List<int> AuthorIds { get; set; } = [];
    public List<int> GenreIds { get; set; } = [];
    public int? PublisherId { get; set; }
}