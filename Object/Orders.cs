using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CSharp_Basic.Object
{
    public class Orders
    {
        public static int Count { get; internal set; }
        public Guid order_id { get; set; }
         public Guid UserId { get; set; }
         public List<CartItem> OrdersItems { get; set; }
        public decimal TotalAmount { get; set; }
        public List<CartItem> OrderItems { get; internal set; }

        
    }
}

