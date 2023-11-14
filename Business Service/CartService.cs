
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSharp_Basic.Business_Service
{
  public  class CartService
    {
        private readonly Dictionary<int, CartItem> cartItems;

        public CartService()
        {
            cartItems = new Dictionary<int, CartItem>();
        }

        public void AddToCart(Products product, int quantity)
        {
            if (product == null)
            {
                Console.WriteLine("Product is null. Cannot add to cart.");
                return;
            }

            if (quantity <= 0)
            {
                Console.WriteLine("Quantity should be greater than zero. Cannot add to cart.");
                return;
            }

            if (cartItems.ContainsKey(product.ProductId))
            {
                // Product already exists in the cart, update quantity
                cartItems[product.ProductId].Quantity += quantity;
            }
            else
            {
                // Product is not in the cart, add it
                var cartItem = new CartItem
                {
                    product_id = product.ProductId,
                    name_product = product.ProductName,
                    Price = product.Price,
                    Quantity = quantity
                };

                cartItems.Add(product.ProductId, cartItem);
            }

            Console.WriteLine($"Added {quantity} {product.ProductName}(s) to the cart.");
        }

        public List<CartItem> GetCartItems()
        {
            return cartItems.Values.ToList();
        }

        public void ClearCart()
        {
            cartItems.Clear();
            Console.WriteLine("Cart cleared.");
        }

        public void DisplayCart()
        {
            Console.WriteLine("Cart Contents:");

            foreach (var cartItem in cartItems.Values)
            {
                Console.WriteLine($"{cartItem.name_product} - Quantity: {cartItem.Quantity}, Price: {cartItem.Price:C}");
            }

            Console.WriteLine();
        }
    }

    
}
