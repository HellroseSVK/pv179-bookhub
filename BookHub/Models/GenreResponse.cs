namespace WebAPI.Models;

public class GenreResponse : BaseResponse
{
    public string Name { get; set; }
    public List<int> BookIds { get; set; }
}