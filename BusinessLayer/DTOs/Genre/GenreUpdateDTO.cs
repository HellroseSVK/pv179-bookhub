namespace BusinessLayer.DTOs.Genre;

public class GenreUpdateDTO : BaseDTO
{
    public string Name { get; set; }

    public List<int> BookIds { get; set; }
}