using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Basic.Object
{
    /// <summary>
    /// Class Product
    /// </summary>
    public class Products
    {
        /// <summary>
        /// Id Product
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public string? name { get; set; } = string.Empty;

        /// <summary>
        /// Price of the product
        /// </summary>
        public decimal Price { get; set; }
        public int product_id { get; internal set; }
        public string name_product { get; internal set; }

        
    }
}