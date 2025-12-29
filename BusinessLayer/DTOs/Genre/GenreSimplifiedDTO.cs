namespace BusinessLayer.DTOs.Genre;

public class GenreSimplifiedDTO : BaseDTO
{
    public required string Name { get; set; }
    public bool IsPrimary { get; set; }
}