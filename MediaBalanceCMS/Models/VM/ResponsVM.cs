using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MediaBalanceCMS.Models.VM.ProductCategoryResponseVM;

namespace MediaBalanceCMS.Models.VM
{
    public class ResponsVM
    {
        public List<CatagoryReguest> catagoryReguests { get; set; }
        public ProductReguest productReguest { get; set; }
    }
}
