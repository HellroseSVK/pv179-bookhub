using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.WishlistItem
{
    public class WishlistItemPartialDTO : BaseDTO
    {
        public int? BookId { get; set; }
        public int? UserId { get; set; }
    }
}
