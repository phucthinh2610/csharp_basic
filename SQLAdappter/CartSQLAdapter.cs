using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CSharp_Basic.Object;

namespace CSharp_Basic.SQLAdappter
{
     public interface CartSQLAdapter  : ISQLAdapter
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        int product_iD { get; set; }
        int UserId { get; }

        public int Delete<T>(Guid id) where T : class, new()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"DELETE FROM {TableName} WHERE cart_id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting cart: {ex.Message}");
                return 0;
            }
        }

        public Cart GetCartByUserId(Guid userId, int empty)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"SELECT cart_id, user_id FROM {TableName} WHERE user_id = @UserId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserId", userId);

                    Guid cartId;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Cart cart = new Cart();
                            if (Guid.TryParse(reader["cart_id"].ToString(), out cartId))
                            {
                                cart.Id = cartId;
                            }
                            else
                            {
                                cart.Id = Guid.Empty;
                            }

                            if (Guid.TryParse(reader["user_id"].ToString(), out  userId))
                            {
                                cart.UserId = UserId;
                            }
                            else
                            {
                                cart.UserId = empty;
                            }

                            return cart;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving cart: {ex.Message}");
            }

            return null;
        }

        public List<T> GetData<T>() where T : class, new()
        {
            throw new NotImplementedException();
        }

        public int Insert<T>(T item) where T : class, new()
        {
            try
            {
                Cart cart = item as Cart;

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"INSERT INTO {TableName} (cart_id, user_id) VALUES (@Id, @UserId)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", cart.Id);
                    command.Parameters.AddWithValue("@UserId", cart.UserId);

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting cart: {ex.Message}");
                return 0;
            }
        }

        public int InsertCartItem(CartItem cartItem)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"INSERT INTO CartItem (cart_item_id, cart_id, product_iD, quantity) " +
                                   "VALUES (@Id, @CartId, @ProductId, @Quantity)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", cartItem.Id);
                    command.Parameters.AddWithValue("@CartId", cartItem.CartId);
                    command.Parameters.AddWithValue("@ProductId", cartItem.ProductId);
                    command.Parameters.AddWithValue("@Quantity", cartItem.Quantity);

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting cart item: {ex.Message}");
                return 0;
            }
        }

        public int Update<T>(T item) where T : class, new()
        {
            throw new NotImplementedException();
        }
    }
}