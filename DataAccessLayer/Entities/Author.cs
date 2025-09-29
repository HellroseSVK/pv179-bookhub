using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }
    }
}