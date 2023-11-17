using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Basic.Business_Service
{
    public class CartItem
    {
        public Guid product_id { get; set; }
        public string name_product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
