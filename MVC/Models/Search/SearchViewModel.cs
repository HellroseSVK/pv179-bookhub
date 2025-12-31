using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Genre;
using BusinessLayer.DTOs.Publisher;

namespace MVC.Models.Search;

public class SearchViewModel
{
    public string? SearchTerm { get; set; }
    public PagedResult<BookDTO>? Books { get; set; }
    public List<GenreDTO>? Genres { get; set; }
    public List<PublisherDTO>? Publishers { get; set; }
    public List<AuthorDTO>? Authors { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}