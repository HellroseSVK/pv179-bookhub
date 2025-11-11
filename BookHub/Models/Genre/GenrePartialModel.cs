namespace WebAPI.Models.Genre;

public class GenrePartialModel
{
    public string? Name { get; set; }
    public List<int>? BookIds { get; set; }
}