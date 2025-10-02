using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Review : BaseEntity
    {
        [Range(1, 5)]
        public int Rating { get; set; }

        public string Body { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public Book Book { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
