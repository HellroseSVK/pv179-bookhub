using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class OrderItem : BaseEntity
{
    [ForeignKey("Book")]
    public int BookId { get; set; }

    public Book? Book { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    public Order? Order { get; set; }
}