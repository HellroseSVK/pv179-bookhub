using BusinessLayer.DTOs.Book;

namespace BusinessLayer.DTOs.Author;

public class AuthorDTO : AuthorSimplifiedDTO
{
    public List<BookSimplifiedDTO> Books { get; set; } = [];
}