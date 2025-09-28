using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class BookGenre : BaseEntity
{
    [ForeignKey("Book")]
    public int BookId { get; set; }

    public Book Book { get; set; }

    [ForeignKey("Genre")]
    public int GenreId { get; set; }

    public Genre Genre { get; set; }
}