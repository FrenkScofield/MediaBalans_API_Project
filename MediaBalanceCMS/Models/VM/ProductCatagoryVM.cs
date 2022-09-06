using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaBalanceCMS.Models.VM
{
    public class ProductCatagoryVM
    {
        public IEnumerable<ProductVM> ProductVMs { get; set; }

        public IEnumerable<CategoryVM> CategoryVMs { get; set; }

        public ProductVM ProductVM { get; set; }
    }
}
