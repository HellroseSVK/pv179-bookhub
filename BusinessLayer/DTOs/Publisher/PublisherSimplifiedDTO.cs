using BusinessLayer.DTOs;

namespace BusinessLayer.DTOs.Publisher;

public class PublisherSimplifiedDTO : BaseDTO
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Website { get; set; }
}