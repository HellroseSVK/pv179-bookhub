using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Publisher;

public class PublisherModel
{
    [Required]
    [StringLength(50, MinimumLength = 1)]
    public required string Name { get; set; }

    [Required]
    public List<int> BookIds { get; set; } = [];
}