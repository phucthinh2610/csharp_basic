using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using CSharp_Basic.Object;

namespace CSharp_Basic.SQLAdappter
{

    public interface UsersSQLAdapter : ISQLAdapter
    {
        public string ConnectionString { get; set; }
        public string TableName { get; set; }






        public void InsertData(USERS user)
        {
            try
            {
                string ConString = @"DESKTOP-42KI5S0\MYSQL;database= HCM23_FRF_FNW_01;intergrated security = True;";
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();

                    string commandText = "INSERT INTO USERS (fullName, UserId, email) VALUES (@fullName, @UserId, @email)";

                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("@fullName", user.fullName);
                        command.Parameters.AddWithValue("@UserId", user.Id);
                        command.Parameters.AddWithValue("@email", user.email);
                        command.ExecuteNonQuery();
                    }
                }
            }




            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void UpdateData(USERS user)
        {
            try
            {
                string ConString = @"DESKTOP-42KI5S0\MYSQL;database= HCM23_FRF_FNW_01;intergrated security = True;";
                using (SqlConnection connection = new SqlConnection(ConString))
                {
                    connection.Open();

                    string commandText = "UPDATE USERS SET fullName = @fullName, email = @email WHERE Id = @Id";

                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.Parameters.AddWithValue("@fullName", user.fullName);
                        command.Parameters.AddWithValue("@email", user.email);
                        command.Parameters.AddWithValue("@Id", user.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
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
    }
}



            
        
    
  