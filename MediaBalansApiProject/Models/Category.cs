using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaBalansApiProject.Models
{
    public class Category :CoreEntity
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
