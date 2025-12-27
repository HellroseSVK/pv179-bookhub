using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Book;

public class BookModel
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public required string Title { get; set; }

    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public required string Description { get; set; }

    //can only be (int, int) or (double, double) not (decimal, decimal)
    [Required]
    [Range(0.00, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    public List<int> AuthorIds { get; set; } = [];

    [Required]
    public List<int> GenreIds { get; set; } = [];

    [Required]
    public int PublisherId { get; set; }
}