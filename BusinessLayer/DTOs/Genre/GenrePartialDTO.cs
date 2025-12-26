namespace BusinessLayer.DTOs.Genre;

public class GenrePartialDTO : BaseDTO
{
    public string? Name { get; set; }

    public List<int>? BookIds { get; set; }
}