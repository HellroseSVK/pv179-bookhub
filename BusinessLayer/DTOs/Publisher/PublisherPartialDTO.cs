namespace BusinessLayer.DTOs.Publisher;

public class PublisherPartialDTO : BaseDTO
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public string? Website { get; set; }

    public List<int>? BookIds { get; set; }
}