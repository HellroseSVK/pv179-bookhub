namespace WebAPI.Models.Author;

public class AuthorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public List<int> BookIds { get; set; }
}