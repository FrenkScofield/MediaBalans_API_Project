using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaBalanceCMS.Models.VM
{
    public class ProductCategoryResponseVM
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class CatagoryReguest
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class ProductReguest
        {
            public int id { get; set; }
            public string name { get; set; }
            public string price { get; set; }
            public string whiting { get; set; }
            public string color { get; set; }
            public int categoryId { get; set; }
            public string categoryName { get; set; }
        }

        public class RootProductCategoryResponseVM
        {
            public List<CatagoryReguest> catagoryReguests { get; set; }
            public ProductReguest productReguest { get; set; }
        }


    }
}
