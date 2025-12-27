using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public decimal Price { get; set; }

    public string? ImagePath { get; set; }

    public List<BookAuthor> BookAuthors { get; set; } = [];

    public List<BookGenre> BookGenres { get; set; } = [];

    public List<Review> Reviews { get; set; } = [];

    public List<WishlistItem> WishlistItems { get; set; } = [];

    public List<OrderItem> OrderItems { get; set; } = [];

    [ForeignKey("Publisher")]
    public int PublisherId { get; set; }

    public Publisher? Publisher { get; set; }

    public int EditCount { get; set; } = 0;

    public string[]? LastModifiedByUserIds { get; set; }
}