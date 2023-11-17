using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CSharp_Basic.Object;
using CSharp_Basic.SQLAdappter;

namespace CSharp_Basic.SQLAdappter
{

    public class UsersSQLAdapter : ISQLAdapter
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }
        string ISQLAdapter.ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        string ISQLAdapter.TableName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public  UsersSQLAdapter( string ConString)
        {
            this.ConnectionString = ConString;
            this.TableName = "USERS";
        }


        public T Get<T>(Guid id) where T : class, new()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = $"SELECT UserId, fullName,  email  FROM {TableName} WHERE user_id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            USERS user = new USERS
                            {
                                Id = Guid.Parse(reader["UserId"].ToString()),
                                fullName = reader["fullName"].ToString(),
                                email = reader["email"].ToString()
                            };

                            return user as T;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving USERS: {ex.Message}");
            }

            return null;
        }



        public int Insert<T>(T item) where T : class, new()
        {
            try
            {
                USERS user = item as USERS;
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO USERS (fullName, UserId, email) VALUES (@fullName, @UserId, @email)";

                    SqlCommand command = new SqlCommand(query, connection);
                    {
                        command.Parameters.AddWithValue("@fullName", user.fullName);
                        command.Parameters.AddWithValue("@UserId", user.Id);
                        command.Parameters.AddWithValue("@email", user.email);
                        return command.ExecuteNonQuery();
                    }
                }
            }




            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return default;
            }
        }

        public int Update<T>(T item) where T : class, new()

        {
            try
            {
                USERS user = item as USERS;
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    string query = "UPDATE USERS SET fullName = @fullName, email = @email WHERE Id = @Id";

                    SqlCommand command = new SqlCommand(query, connection);
                    {
                        command.Parameters.AddWithValue("@fullName", user.fullName);
                        command.Parameters.AddWithValue("@email", user.email);
                        command.Parameters.AddWithValue("@Id", user.Id);
                        return command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Delete user
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

                    string query = $"DELETE FROM {TableName} WHERE user_id = @Id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Get data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public  List<USERS> GetData()
        {
            try
            {


                List<USERS> usersList = new List<USERS>();



                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();

                    // Thay đổi tên bảng thành "app_users"
                    string query = $"SELECT * FROM USERS";

                    using (SqlCommand command = new SqlCommand(query, connection))

                    
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            {
                                while (reader.Read())
                                {
                                    USERS user = new USERS
                                    {
                                        Id = Guid.Parse(reader["UserId"].ToString()),
                                        fullName = reader["fullName"].ToString(),

                                        email = reader["email"].ToString(),

                                    };

                                    usersList.Add(user);
                                }
                            }
                        }
                    }
                    return usersList;
                }
            
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving users: {ex.Message}");
                return null;
            }
        }

        List<T> ISQLAdapter.GetData<T>()
        {
            throw new NotImplementedException();
        }

        T ISQLAdapter.Get<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        int ISQLAdapter.Insert<T>(T item)
        {
            throw new NotImplementedException();
        }

        int ISQLAdapter.Update<T>(T item)
        {
            throw new NotImplementedException();
        }

        int ISQLAdapter.Delete<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        internal List<T> GetData<T>()
        {
            throw new NotImplementedException();
        }
    }
}



            
        
    
  