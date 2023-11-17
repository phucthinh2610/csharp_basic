using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CSharp_Basic.Object;
using System.Linq;
using System.Text;

namespace CSharp_Basic.SQLAdappter
{
    /// <summary>
    /// Product Adapter
    /// </summary>
    public class ProductsSQLAdapter : ISQLAdapter
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }

        public ProductsSQLAdapter(string connectionString)
        {
            this.ConnectionString = connectionString;
            this.TableName = "Products";
        }


            /// <summary>
            /// Delete Product
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="id"></param>
            /// <returns></returns>
            public int Delete<T>(Guid id) where T : class, new()
             {
                 try
                    {
                     using (SqlConnection connection = new SqlConnection())
                    {
                     connection.Open();

                        string query = $"DELETE FROM {TableName} WHERE product_id = @Id";
                        SqlCommand command = new SqlCommand(query, connection);
                         command.Parameters.AddWithValue("@Id", id);

                        return command.ExecuteNonQuery();
                    }
             }
                 catch (Exception ex)
                 {
                    Console.WriteLine($"Error deleting product: {ex.Message}");
                 return 0;
            }
        }


        /// <summary>
        /// Get T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(Guid id) where T : class, new()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"SELECT product_id, name_product, price FROM {TableName} WHERE product_id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Products product = new Products
                            {
                                Id = Guid.Parse(reader["product_id"].ToString()),
                                name = reader["name_product"].ToString(),
                                Price = Convert.ToDecimal(reader["price"])
                            };

                            return product as T;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving product: {ex.Message}");
            }

            return null;
        }


        /// <summary>
        /// Get Data Product
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetData<T>() where T : class, new()
        {
            List<T> product = new List<T>();

            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"SELECT product_id, name_product, price FROM {TableName}";
                    SqlCommand command = new SqlCommand(query, connection);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Products products = new Products
                            {
                                Id = Guid.Parse(reader["product_id"].ToString()),
                                name = reader["name_product"].ToString(),
                                Price = Convert.ToDecimal(reader["price"])
                            };

                            product.Add(products as T);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving products: {ex.Message}");
            }

            return product;
        }


        /// <summary>
        /// Insert Product
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Insert<T>(T item) where T : class, new()
        {
            try
            {
                Products product = item as Products;

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"INSERT INTO {TableName} (product_id, name_product, Price) VALUES (@Id, @Name, @Price)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", product.Id);
                    command.Parameters.AddWithValue("@Name", product.name);
                    command.Parameters.AddWithValue("@Price", product.Price);

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting product: {ex.Message}");
                return 0;
            }
        }


        /// <summary>
        /// Update Product
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update<T>(T item) where T : class, new()
        {
            try
            {
                Products product = item as Products;

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"UPDATE {TableName} SET name_product = @Name, Price = @Price WHERE product_id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", product.name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Id", product.Id);

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating product: {ex.Message}");
                return 0;
            }
        }
    }
}