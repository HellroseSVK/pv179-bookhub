namespace BusinessLayer.DTOs.Genre;

public class GenreUpsertDTO : BaseDTO
{
    public required string Name { get; set; }

    public List<int> BookIds { get; set; } = [];
}