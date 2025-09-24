using System;
using System.Collections.Generic;
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
    }
}
