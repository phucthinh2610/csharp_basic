using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CSharp_Basic.Object;

namespace CSharp_Basic.SQLAdappter
{
    public interface OrdersSQLAdapter : ISQLAdapter
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        int ProductId { get; }

        public int Delete<T>(Guid id) where T : class, new()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"DELETE FROM {TableName} WHERE order_id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting orders: {ex.Message}");
                return 0;
            }
        }

        public Orders GetOrdersByOrdersId(Guid orderId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"SELECT order_id, user_id FROM {TableName} WHERE order_id = @OrdersId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@OrdersId", orderId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Orders order = new Orders
                            {
                                order_id = Guid.Parse(reader["order_id"].ToString()),
                                UserId = Guid.Parse(reader["user_id"].ToString())
                            };

                            return order;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving order: {ex.Message}");
            }

            return null;
        }
    }
}
