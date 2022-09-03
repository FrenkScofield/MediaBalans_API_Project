using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaBalansApiProject.Models
{
    public class Product: CoreEntity
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Whiting { get; set; }
        public string Color { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
