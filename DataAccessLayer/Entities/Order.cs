using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Enums;

namespace DataAccessLayer.Entities
{
    public class Order : BaseEntity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User? User { get; set; }

        public List<OrderItem> OrderItems { get; set; } = [];
        public GiftCode? GiftCode { get; set; }

        public OrderStatus OrderStatus { get; set; } = OrderStatus.Draft;
    }
}
