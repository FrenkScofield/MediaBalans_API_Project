using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaBalanceCMS.Models.VM
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Whiting { get; set; }
        public string Color { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
