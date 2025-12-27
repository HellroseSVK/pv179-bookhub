namespace MVC.Models.Book;

public class BookListViewModel
{
    public List<BookViewModel> Books { get; set; } = [];
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public int PageSize { get; set; } = 12;
    public int TotalBooks { get; set; } = 0;
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public List<GenreViewModel> Genres { get; set; } = [];
    public int? SelectedGenreId { get; set; }
}