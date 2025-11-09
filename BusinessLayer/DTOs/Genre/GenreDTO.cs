using BusinessLayer.DTOs.Book;

namespace BusinessLayer.DTOs.Genre;

public class GenreDTO : GenreSimplifiedDTO
{
    public List<BookSimplifiedDTO> Books { get; set; }
}