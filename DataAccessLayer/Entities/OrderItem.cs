using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities
{
    public class OrderItem : BaseEntity
    {
        [ForeignKey("Book")]
        public int BookId { get; set; }

        public Book? Book { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User? User { get; set; }
    }
}
