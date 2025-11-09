using BusinessLayer.DTOs.Author;

namespace BusinessLayer.DTOs.Genre;

public class GenreCreateDTO : AuthorSimplifiedDTO
{
    public List<int> BookIds { get; set; }
}