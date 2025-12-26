namespace WebAPI.Models.Publisher;

public class PublisherModel
{
    public required string Name { get; set; }

    public List<int> BookIds { get; set; } = [];
}