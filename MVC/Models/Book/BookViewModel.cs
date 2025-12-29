using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Genre;
using BusinessLayer.DTOs.Publisher;

namespace MVC.Models.Book;

public class BookViewModel
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public PublisherSimplifiedDTO? Publisher { get; set; }
    public List<AuthorSimplifiedDTO> Authors { get; set; } = [];
    public List<GenreSimplifiedDTO> Genres { get; set; } = [];

    public GenreSimplifiedDTO? PrimaryGenre => Genres.FirstOrDefault(g => g.IsPrimary);
    public List<GenreSimplifiedDTO> SecondaryGenres => Genres.Where(g => !g.IsPrimary).ToList();
}