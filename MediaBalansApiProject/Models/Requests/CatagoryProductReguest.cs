using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaBalansApiProject.Models.Requests
{
    public class CatagoryProductReguest
    {
        public IEnumerable<CatagoryReguest>  CatagoryReguests { get; set; }

        public ProductReguest ProductReguest { get; set; }

        
    }
}
