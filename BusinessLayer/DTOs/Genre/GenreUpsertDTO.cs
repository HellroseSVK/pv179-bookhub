namespace BusinessLayer.DTOs.Genre;

public class GenreUpsertDTO
{
    public required string Name { get; set; }

    public List<int> BookIds { get; set; } = [];
}