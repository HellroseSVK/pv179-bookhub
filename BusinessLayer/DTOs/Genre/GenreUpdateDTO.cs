namespace BusinessLayer.DTOs.Genre;

public class GenreUpdateDTO : BaseDTO
{
    public required string Name { get; set; }

    public List<int> BookIds { get; set; } = [];
}