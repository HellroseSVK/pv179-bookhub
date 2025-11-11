namespace BusinessLayer.DTOs.Author;

public class AuthorUpsertDTO : BaseDTO
{
    public required string Name { get; set; }
    public required string Surname { get; set; }

    public List<int> BookIds { get; set; } = [];
}