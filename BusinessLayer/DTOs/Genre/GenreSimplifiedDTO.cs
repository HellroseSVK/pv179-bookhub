using BusinessLayer.DTOs;

namespace BusinessLayer.DTOs.Genre;

public class GenreSimplifiedDTO : BaseDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
}