namespace BusinessLayer.DTOs.Author;

public class AuthorPartialDTO : BaseDTO
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    
    public List<int>? BookIds { get; set; }
}