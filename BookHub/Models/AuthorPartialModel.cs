namespace WebAPI.Models;

public class AuthorPartialModel
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public List<int>? BookIds { get; set; }
}