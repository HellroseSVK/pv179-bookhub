namespace WebAPI.Models.Author;

public class AuthorModel
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public List<int> BookIds { get; set; } = [];
}