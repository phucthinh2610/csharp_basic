using System;
using System.Collections.Generic;

namespace CSharp_Basic.Object
{
    /// <summary>
    /// Class Cart
    /// </summary>
    public class Cart
    {
        internal Guid product_id;

        /// <summary>
        /// Id User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// List of items in the cart
        /// </summary>
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public Guid Id { get; internal set; }
    }   

    /// <summary>
    /// Class CartItem
    /// </summary>
    public class CartItem
    {
        /// <summary>
        /// Id Product
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Quantity in the cart
        /// </summary>
        public int Quantity { get; set; }
        public int Price { get; internal set; }
        public object ProductName { get; internal set; }
        public Guid Id { get; internal set; }
        public Guid CartId { get; internal set; }
    }
}