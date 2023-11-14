
using CSharp_Basic.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSharp_Basic.Business_Service
{
    public class OrdersService
    {
        private const int V = 1;
        private readonly List<Orders> orders;

        public OrdersService()
        {
            orders = new List<Orders>();
        }

        public List<Orders> GetOrders()
        {
            return orders;
        }

        public void CreateUserOrders(int userId, CartService cartService, List<Orders> orders, Orders orders)
        {
            // Bước 1: Lấy các mục từ giỏ hàng
            List<Object.CartItem> cartItems = cartService.GetCartItems();

            if (cartItems.Count == 0)
            {
                Console.WriteLine("Error: The cart is empty. Cannot create an order.");
                return;
            }

            // Bước 2: Tạo đơn đặt hàng
            int userId1 = userId;
            Orders order = new Orders
            {
                order_id = orders.Count + V, // Tạo OrderId một cách đơn giản
                UserId = userId1,
                OrderItems = cartItems,
                TotalAmount = cartItems.Sum(item => item.Price * item.Quantity)
            };

            // Bước 3: Thêm đơn đặt hàng vào danh sách đơn đặt hàng
            orders.Add(order);

            // Bước 4: Hiển thị thông báo và xóa sản phẩm từ giỏ hàng
            Console.WriteLine($"Order created successfully. OrderId: {order.order_id}, TotalAmount: {order.TotalAmount:C}");
            cartService.ClearCart();
        }

        public void DisplayOrders()
        {
            Console.WriteLine("Orders:");

            foreach (var order in orders)
            {
                Console.WriteLine($"OrderId: {order.order_id}, UserId: {order.UserId}, TotalAmount: {order.TotalAmount:C}");
                Console.WriteLine("Order Items:");

                foreach (var item in order.OrderItems)
                {
                    Console.WriteLine($"  ProductId: {item.ProductId}, ProductName: {item.ProductName}, " +
                                      $"Price: {item.Price:C}, Quantity: {item.Quantity}");
                }

                Console.WriteLine();
            }
        }
    }

    public class CartService
    {
        internal void ClearCart()
        {
            throw new NotImplementedException();
        }

        internal List<CartItem> GetCartItems()
        {
            throw new NotImplementedException();
        }

        internal void AddToCart(Products laptop, int v)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main()
        {
            // Bước 1: Tạo đối tượng CartService và OrderService
            CartService cartService = new CartService();
            OrderService orderService = new OrderService();

            // Bước 2: Tạo sản phẩm
            Products laptop = new Products { product_id = 1, name_product = "Laptop", Price = 120000 };
            Products phone = new Products { product_id = 2, name_product = "Phone", Price = 80000 };

            // Bước 3: Thêm sản phẩm vào giỏ hàng
            cartService.AddToCart(laptop, 2);
            cartService.AddToCart(phone, 1);

            // Bước 4: Hiển thị nội dung giỏ hàng
            cartService.DisplayCart();

            // Bước 5: Tạo đơn đặt hàng cho người dùng và xóa sản phẩm khỏi giỏ hàng
            orderService.CreateUserOrder(1, cartService);

            // Bước 6: Hiển thị danh sách đơn đặt hàng
            orderService.DisplayOrders();
        }
    }
}
