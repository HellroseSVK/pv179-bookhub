using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }
        public List<BookGenre> BookGenres { get; set; }

        public List<Review> Reviews { get; set; }

        public List<Wishlist> Wishlists { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }
    }
}