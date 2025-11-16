using BusinessLayer.DTOs.Book;

namespace BusinessLayer.DTOs.Publisher;

public class PublisherDTO : PublisherSimplifiedDTO
{
    public List<BookSimplifiedDTO> Books { get; set; } = [];
}