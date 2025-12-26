namespace BusinessLayer.DTOs.Author;

public class AuthorSimplifiedDTO : BaseDTO
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
}