using CSharp_Basic.Object;
using CSharp_Basic.SQLAdappter;
using CSharp_Basic.Object;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CSharp_Basic.Object;

namespace CSharp_Basic.SQLAdapter
{
    public class OrderDetailSqlAdapter : ISQLAdapter
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }

        public OrderDetailSqlAdapter(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.TableName = "Order_details";
        }

        public int Insert<T>(T item) where T : class, new()
        {
            try
            {
                Orders order = item as Orders;

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"INSERT INTO {TableName} (order_id, UserId, total_amount) VALUES (@Id, @userId, @TotalAmount)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", order.Id);
                    command.Parameters.AddWithValue("@UserId", order.UserId);
                    
                    command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting order: {ex.Message}");
                return 0;
            }
        }

        public T Get<T>(Guid id) where T : class, new()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM {TableName} WHERE id_order = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Orders order = new Orders
                        {
                            Id = Guid.Parse(reader["id_order"].ToString()),
                            UserId = Guid.Parse(reader["user_id"].ToString()),
                            OrderDay = DateTime.Parse(reader["order_day"].ToString()),
                            TotalAmount = decimal.Parse(reader["total_amount"].ToString())
                        };

                        return order as T;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting order: {ex.Message}");
            }

            return null;
        }

        public List<T> GetData<T>() where T : class, new()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM {TableName}";
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();

                    List<T> orders = new List<T>();

                    while (reader.Read())
                    {
                        Orders order = new Orders
                        {
                            Id = Guid.Parse(reader["id_order"].ToString()),
                            UserId = Guid.Parse(reader["user_id"].ToString()),
                            OrderDay = DateTime.Parse(reader["order_day"].ToString()),
                            TotalAmount = decimal.Parse(reader["total_amount"].ToString())
                        };

                        orders.Add(order as T);
                    }

                    return orders;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting orders: {ex.Message}");
            }

            return null;
        }

        public int Update<T>(T item) where T : class, new()
        {
            try
            {
                Orders order = item as Orders;

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"UPDATE {TableName} SET user_id = @UserId, order_day = @OrderDay, total_amount = @TotalAmount WHERE id_order = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", order.Id);
                    command.Parameters.AddWithValue("@UserId", order.UserId);
                    command.Parameters.AddWithValue("@OrderDay", order.OrderDay);
                    command.Parameters.AddWithValue("@TotalAmount", order.TotalAmount);

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating order: {ex.Message}");
                return 0;
            }
        }

        public int Delete<T>(Guid id) where T : class, new()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"DELETE FROM {TableName} WHERE id_order = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting order: {ex.Message}");
                return 0;
            }
        }
    }
}