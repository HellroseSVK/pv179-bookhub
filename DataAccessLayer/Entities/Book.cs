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

        public string Description {  get; set; }
        
        public decimal Price {  get; set; }
        
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        
        public Author Author { get; set; }

        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public List<Review> Reviews { get; set; }
    }
}
