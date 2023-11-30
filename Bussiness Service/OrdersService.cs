
using CSharp_Basic.Object;
using CSharp_Basic.SQLAdappter;
using CSharp_Basic.SQLAdapter;
using System;
using System.Collections.Generic;





namespace CSharp_Basic.BussinessService
{
    public class OrderService
    {
        private readonly OrderSqlAdapter orderAdapter;
        private readonly OrderDetailSqlAdapter orderDetailSqlAdapter;
        private readonly CartService cartService;
        private readonly CartSQLAdapter cartSQLAdapter;
        private readonly CartDetailSqlAdapter cartDetailSqlAdapter;

        public OrderService(string connectionString)
        {
            this.orderAdapter = new OrderSqlAdapter(connectionString);
            this.orderDetailSqlAdapter = new OrderDetailSqlAdapter(connectionString);
            this.cartService = new CartService(connectionString);
            this.cartSQLAdapter = new CartSQLAdapter(connectionString);
            this.cartDetailSqlAdapter = new CartDetailSqlAdapter(connectionString);
        }

        public void ClearUSERSCart(Guid userId)
        {
            Cart userCart = GetUSERSCart(userId);

            if (userCart != null)
            {
                List<CartDetail> cartDetails = GetCartDetailsByCartId(userCart.Id);
                foreach (var cartDetail in cartDetails)
                {
                    cartDetailSqlAdapter.Delete<CartDetail>(cartDetail.Id);
                }

                cartSQLAdapter.Delete<Cart>(userCart.Id);

                Console.WriteLine($"USERS cart cleared. USERS ID: {userId}");
            }
            else
            {
                Console.WriteLine($"USERS cart not found for USERS ID: {userId}");
            }
        }

        private Cart GetUSERSCart(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void CreateOrder(Guid customerId)
        {
            // Retrieve the customer's cart details
            List<CartDetail> cartDetails = cartService.GetCartDetails(customerId);

            if (cartDetails != null && cartDetails.Any())
            {
                // Calculate the total amount
                decimal totalAmount = 0;
                foreach (var cartDetail in cartDetails)
                {
                    Products product = cartService.GetProductById(cartDetail.ProductId);
                    if (product != null)
                    {
                        totalAmount += cartDetail.Quantity * product.Price;
                    }
                }

                // Create an order
                Orders newOrder = new Orders
                {
                    Id = Guid.NewGuid(),
                    UserId =  ,
                    OrderDay = DateTime.Now, // Set the order date to the current date
                    TotalAmount = totalAmount
                };

                // Insert the order into the database
                int result = orderAdapter.Insert(newOrder);

                if (result > 0)
                {
                    Console.WriteLine("Order created successfully.");

                    // Clear the user's cart after creating the order
                    cartService.ClearUSERSCart(userId);
                }
                else
                {
                    Console.WriteLine("Error creating order.");
                }
            }
            else
            {
                Console.WriteLine("No items in the customer's cart. Unable to create an order.");
            }
        }


        public void ViewOrders()
        {
            // Retrieve and display all orders
            List<Orders> orders = orderAdapter.GetData<Orders>();

            if (orders != null && orders.Any())
            {
                Console.WriteLine("Orders:");

                foreach (var order in orders)
                {
                    Console.WriteLine($"Order ID: {order.Id}, User ID: {order.UserId}, Order Date: {order.OrderDay}, Total Amount: {order.TotalAmount}");
                }
            }
            else
            {
                Console.WriteLine("No orders found.");
            }
        }

        public void CalculateTotalAmount(List<CartDetail> cartDetails)
        {
            // Calculate total amount based on product prices and quantities
            decimal totalAmount = 0;

            foreach (var cartDetail in cartDetails)
            {
                Products product = cartService.GetProductById(cartDetail.ProductId);
                if (product != null)
                {
                    totalAmount += cartDetail.Quantity * product.Price;
                }
            }

            Console.WriteLine($"Total amount: {totalAmount}");
        }

        private Cart GetUSERSCART(Guid userId)
        {
            // Get user cart from the database
            List<Cart> userCarts = cartSQLAdapter.GetData<Cart>();
            return userCarts.FirstOrDefault(c => c.UserId == userId);
        }

        private List<CartDetail> GetCartDetailsByCartId(Guid Id)
        {
            // Get cart details based on cartId
            List<CartDetail> cartDetail = cartDetailSqlAdapter.GetData<CartDetail>().Where(cd => cd.Id == Id).ToList();
            return cartDetail;
        }
    }

    public class CartService
    {
        private string connectionString;
        private object connectionString1;

        public CartService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public CartService(object connectionString1)
        {
            this.connectionString1 = connectionString1;
        }

        public void ClearUSERSCart(Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<CartDetail> GetCartDetails(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Products GetProductById(Guid productId)
        {
            throw new NotImplementedException();
        }

       public List<Cart> GetUSERSCarts()
        {
            throw new NotImplementedException();
        }

        public void ViewUSERSCart(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void AddProductsToCart(Guid userId, Guid productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public void RemoveProductFromCart(Guid userId, Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}