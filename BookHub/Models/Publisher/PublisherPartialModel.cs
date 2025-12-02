using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Publisher;

public class PublisherPartialModel
{
    [StringLength(50, MinimumLength = 1)]
    public string? Name { get; set; }

    public List<int>? BookIds { get; set; }
}