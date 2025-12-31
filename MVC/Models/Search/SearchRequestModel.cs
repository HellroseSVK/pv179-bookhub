namespace MVC.Models.Search;

public class SearchRequestModel
{
    public string? SearchTerm { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}