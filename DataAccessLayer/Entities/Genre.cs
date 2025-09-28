using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }

        public List<BookGenre> BookGenres { get; set; }
    }
}
