namespace BusinessLayer.DTOs.Author;

public class AuthorCreateDTO : AuthorSimplifiedDTO
{
    public List<int> BookIds { get; set; }
}