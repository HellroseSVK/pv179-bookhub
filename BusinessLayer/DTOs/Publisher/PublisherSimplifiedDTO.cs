using BusinessLayer.DTOs;

namespace BusinessLayer.DTOs.Publisher;

public class PublisherSimplifiedDTO : BaseDTO
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string Website { get; set; }
}