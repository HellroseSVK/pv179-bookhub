using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class BookAuthor : BaseEntity
{
    [ForeignKey("Book")]
    public int BookId { get; set; }

    public Book? Book { get; set; }

    [ForeignKey("Author")]
    public int AuthorId { get; set; }

    public Author? Author { get; set; }
}