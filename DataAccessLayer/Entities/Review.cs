using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Review : BaseEntity
    {
        public double Rating { get; set; }
        public string Body { get; set; }
    }
}
