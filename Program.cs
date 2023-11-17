using System;
using System.Collections.Generic;
using CSharp_Basic.SQLAdappter;
using CSharp_Basic.Object;
using CSharp_Basic.BussinessService;
using System.Data.SqlClient;
using System.Linq;


namespace CSharp_Basic
{
    class Program
    {
        private static object connectionString;

        static void Main()
        {
            string ConString = @"DESKTOP - 42KI5S0\MYSQL;Database=HCM23_FRF_FNW_01;Integrated Security=True;";
            CartService cartService = new CartService(connectionString);
           


            bool exit = false;
            do
            {
                Console.WriteLine("Select table:");
                Console.WriteLine("1. USERS");
                Console.WriteLine("2. Products");
                Console.WriteLine("3. Cart");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        HandleUSERSTable(ConString);
                        break;
                    case 2:
                        HandleProductTable(connectionString);
                        break;
                    case 3:
                        ManageCart(cartService);
                        break;
                    case 0:
                        Console.WriteLine("Exiting in 5 seconds...");
                        System.Threading.Thread.Sleep(5000);
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            } while (!exit);
        }

        static void HandleUSERSTable(string ConString)
        {
            var userAdapter = new UsersSQLAdapter(ConString);

            Console.WriteLine("Select operation:");
            Console.WriteLine("1. View USERS");
            Console.WriteLine("2. Add USERS");
            Console.WriteLine("3. Update USERS");
            Console.WriteLine("4. Delete USERS");
            Console.WriteLine("0. Back");

            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ViewUSERS(userAdapter);
                    break;
                case 2:
                    AddUSERS(userAdapter);
                    break;
                case 3:
                    UpdateUSERS(userAdapter);
                    break;
                case 4:
                    DeleteUSERS(userAdapter);
                    break;
                case 0:
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        static void ViewUSERS(UsersSQLAdapter userAdapter)
        {
            List<USERS> users = userAdapter.GetData<USERS>();

            if (users.Count > 0)
            {
                Console.WriteLine("USERS:");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.UserId}, fullName: {user.fullName} , email: {user.email}");
                }
            }
            else
            {
                Console.WriteLine("No USERS found.");
            }
        }

        static void AddUSERS(UsersSQLAdapter userAdapter)
        {
            Console.Write("Enter full name: ");
            string fullName = Console.ReadLine();
            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            USERS newUsers = new USERS
            {
                UserId = Guid.NewGuid(),
                fullName = fullName,
                email = email,
            };

            int result = userAdapter.Insert(newUsers);

            if (result > 0)
            {
                Console.WriteLine("USERS added successfully.");
            }
            else
            {
                Console.WriteLine("Error adding USERS.");
            }
        }

        static void UpdateUSERS(UsersSQLAdapter userAdapter)
        {
            Console.Write("Enter USERS ID to update: ");
            Guid userId = Guid.Parse(Console.ReadLine());

            USERS existingUSERS = userAdapter.Get<USERS>(userId);

            if (existingUSERS != null)
            {
                Console.Write("Enter new full name (press Enter to keep current): ");
                string newfullName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newfullName))
                {
                    existingUSERS.fullName = newfullName;
                }

                Console.Write("Enter new email (press Enter to keep current): ");
                string newemail = Console.ReadLine();
                if (!string.IsNullOrEmpty(newemail))
                {
                    existingUSERS.email = newemail;
                }


            }

            int result = userAdapter.Update(existingUSERS);

            if (result > 0)
            {
                Console.WriteLine("USERS updated successfully.");
            }
            else
            {
                Console.WriteLine("Error updating USERS.");
            }
        }

        static void DeleteUSERS(UsersSQLAdapter userAdapter)
        {
            Console.Write("Enter USERS ID to delete: ");
            Guid userId = Guid.Parse(Console.ReadLine());

            int result = userAdapter.Delete<USERS>(userId);

            if (result > 0)
            {
                Console.WriteLine("USERS deleted successfully.");
            }
            else
            {
                Console.WriteLine("Error deleting USERS.");
            }
        }

        static void HandleProductTable(string connectionString)
        {
            ISQLAdapter productAdapter = new ProductsSQLAdapter(connectionString);

            Console.WriteLine("Products Table Operations:");
            Console.WriteLine("1. View Products");
            Console.WriteLine("2. Add Products");
            Console.WriteLine("3. Update Products");
            Console.WriteLine("4. Delete Products");

            Console.Write("Enter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    ViewProducts(productAdapter);
                    break;
                case 2:
                    AddProducts(productAdapter);
                    break;
                case 3:
                    UpdateProducts(productAdapter);
                    break;
                case 4:
                    DeleteProducts(productAdapter);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        static void ViewProducts(ISQLAdapter productAdapter)
        {
            List<Products> products = productAdapter.GetData<Products>();
            Console.WriteLine("Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.name}, Price: {product.Price}");
            }
        }

        static void AddProducts(ISQLAdapter productAdapter)
        {
            Console.Write("Enter products name: ");
            string name = Console.ReadLine();

            Console.Write("Enter products price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());

            Products newProduct = new Products
            {
                Id = Guid.NewGuid(),
                name_product = name,
                Price = price
            };

            int result = productAdapter.Insert(newProduct);

            if (result > 0)
                Console.WriteLine("Products added successfully.");
            else
                Console.WriteLine("Failed to add products.");
        }

        static void UpdateProducts(ISQLAdapter productAdapter)
        {
            Console.Write("Enter product ID to update: ");
            Guid productId = Guid.Parse(Console.ReadLine());

            Products existingProduct = productAdapter.Get<Products>(productId);

            if (existingProduct != null)
            {
                Console.Write("Enter new product name: ");
                existingProduct.name = Console.ReadLine();

                Console.Write("Enter new product price: ");
                existingProduct.Price = Convert.ToDecimal(Console.ReadLine());

                int result = productAdapter.Update(existingProduct);

                if (result > 0)
                    Console.WriteLine("Products updated successfully.");
                else
                    Console.WriteLine("Failed to update products.");
            }
            else
            {
                Console.WriteLine("Products not found.");
            }
        }

        static void DeleteProducts(ISQLAdapter productAdapter)
        {
            Console.Write("Enter product ID to delete: ");
            Guid productId = Guid.Parse(Console.ReadLine());

            int result = productAdapter.Delete<Products>(productId);

            if (result > 0)
                Console.WriteLine("Products deleted successfully.");
            else
                Console.WriteLine("Failed to delete products.");
        }

        static void ManageCart(CartService cartService)
        {
            bool exit = false;
            do
            {
                Console.WriteLine("Cart Menu:");
                Console.WriteLine("1. View USERS Carts");
                Console.WriteLine("2. View USERS Cart");
                Console.WriteLine("3. Add Product to USERS Cart");
                Console.WriteLine("4. Remove Product from USERS Cart");
                Console.WriteLine("0. Back to Main Menu");

                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        ViewUSERSCarts(cartService);
                        break;
                    case 2:
                        ViewUSERSCart(cartService);
                        break;
                    case 3:
                        AddProductToCustomerCart(cartService);
                        break;
                    case 4:
                        RemoveProductFromCustomerCart(cartService);
                        break;
                    case 0:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            } while (!exit);
        }

        static void ViewUSERSCarts(CartService cartService)
        {
            List<Cart> usersCarts = cartService.GetUSERSCarts();
            Console.WriteLine("USERS Carts:");
            foreach (var usersCart in usersCarts)
            {
                Console.WriteLine($"Cart ID: {usersCart.Id}, USERS ID: {usersCart.UserId}");
            }
        }

        static void ViewUSERSCart(CartService cartService)
        {
            Console.Write("Enter USERS ID to view cart: ");
            Guid userId = Guid.Parse(Console.ReadLine());

            cartService.ViewUSERSCart(userId);
        }

        static void AddProductToCustomerCart(CartService cartService)
        {
            Console.Write("Enter customer ID: ");
            Guid customerId = Guid.Parse(Console.ReadLine());

            Console.Write("Enter product ID: ");
            Guid productId = Guid.Parse(Console.ReadLine());

            Console.Write("Enter quantity: ");
            int quantity = Convert.ToInt32(Console.ReadLine());

            cartService.AddProductToCart(customerId, productId, quantity);
            Console.WriteLine("Product added to customer cart.");   
        }

         static void RemoveProductFromCustomerCart(CartService cartService)
        {
            try
            {
                Console.Write("Enter customer ID: ");
                string customerIdInput = Console.ReadLine();

                if (Guid.TryParse(customerIdInput, out Guid customerId))
                {
                    Console.Write("Enter product ID: ");
                    string productIdInput = Console.ReadLine();

                    if (Guid.TryParse(productIdInput, out Guid productId))
                    {
                        cartService.RemoveProductFromCart(customerId, productId);
                        Console.WriteLine("Product removed from customer cart.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid product ID. Please enter a valid GUID.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid customer ID. Please enter a valid GUID.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        /// <summary>
        /// ManageOrders
        /// </summary>
        /// <param name="cartService"></param>
        /// <param name="orderService"></param>
        static void ManageOrders(CartService cartService, OrderService orderService)
        {
            bool exit = false;
            do
            {
                Console.WriteLine("Order Management:");
                Console.WriteLine("1. Create Order");
                Console.WriteLine("2. View Orders");
                Console.WriteLine("0. Back");

                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CreateOrder(orderService);
                        break;
                    case 2:
                        ViewOrders(orderService);
                        break;
                    case 0:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

            } while (!exit);
        }

        /// <summary>
        /// CreateOrder
        /// </summary>
        /// <param name="orderService"></param>
        static void CreateOrder(OrderService orderService)
        {
            Console.Write("Enter customer ID: ");
            Guid customerId = Guid.Parse(Console.ReadLine());

            orderService.CreateOrder(customerId);
        }

        /// <summary>
        /// ViewOrders
        /// </summary>
        /// <param name="orderService"></param>
        static void ViewOrders(OrderService orderService)
        {
            orderService.ViewOrders();
        }
    }
}
    }
}
    }
}
        
    
        
        
        
            
        
    


    



