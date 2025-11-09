namespace BusinessLayer.DTOs.Author;

public class AuthorUpdateDTO : BaseDTO
{
    public string Name { get; set; }
    public string Surname { get; set; }
    
    public List<int> BookIds { get; set; }
}