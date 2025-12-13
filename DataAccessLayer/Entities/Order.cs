using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Order : BaseEntity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }

        public User? User { get; set; }

        public List<OrderItem> OrderItems { get; set; } = [];
    }
}
