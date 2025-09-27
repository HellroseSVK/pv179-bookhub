using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public List<Review> Reviews { get; set; }

        [ForeignKey("Wishlist")]
        public int WishlistId { get; set; }

        public Wishlist Wishlist { get; set; }
    }
}
