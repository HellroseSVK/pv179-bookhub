using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Genre;
using BusinessLayer.DTOs.Publisher;
using BusinessLayer.DTOs.Review;
using BusinessLayer.DTOs.WishlistItem;
using BusinessLayer.DTOs.OrderItem;

namespace BusinessLayer.DTOs.Book;

public class BookDTO : BaseDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int PublisherId { get; set; }
    public PublisherSimplifiedDTO? Publisher { get; set; }
    public List<AuthorSimplifiedDTO> Authors { get; set; } = new();
    public List<GenreSimplifiedDTO> Genres { get; set; } = new();
    public List<ReviewSimplifiedDTO> Reviews { get; set; } = new();
    public List<WishlistItemDTO> WishlistItems { get; set; } = new();
    public List<OrderItemDTO> OrderItems { get; set; } = new();
}