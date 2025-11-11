namespace WebAPI.Models.Genre;

public class GenreModel
{
    public required string Name { get; set; }
    public List<int> BookIds { get; set; } = [];
}