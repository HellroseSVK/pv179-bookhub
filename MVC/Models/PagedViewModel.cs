namespace MVC.Models;

public class PagedViewModel<T>
{
    public List<T> Items { get; set; } = [];
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; } = 1;
    public int PageSize { get; set; } = 12;
    public int TotalItems { get; set; } = 0;
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
}
