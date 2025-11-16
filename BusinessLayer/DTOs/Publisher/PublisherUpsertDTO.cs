namespace BusinessLayer.DTOs.Publisher;

public class PublisherUpsertDTO
{
    public required string Name { get; set; }
    public List<int> BookIds { get; set; } = [];
}