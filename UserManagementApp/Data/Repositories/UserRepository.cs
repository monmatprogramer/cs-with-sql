using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UserManagementApp.Models;
using UserManagementApp.Utils;

namespace UserManagementApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = DatabaseConfig.ConnectionString;
        }

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id, username, password, email, full_name, created_at FROM users";
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User user = new User
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString(),
                            Email = reader["email"].ToString(),
                            FullName = reader["full_name"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["created_at"])
                        };

                        users.Add(user);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error getting users: {ex.Message}");
                }
            }

            return users;
        }

        public User GetUserById(int id)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id, username, password, email, full_name, created_at FROM users WHERE id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new User
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString(),
                            Email = reader["email"].ToString(),
                            FullName = reader["full_name"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["created_at"])
                        };
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error getting user by ID: {ex.Message}");
                }
            }

            return user;
        }

        public User GetUserByUsername(string username)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id, username, password, email, full_name, created_at FROM users WHERE username = @Username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        user = new User
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString(),
                            Email = reader["email"].ToString(),
                            FullName = reader["full_name"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["created_at"])
                        };
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error getting user by username: {ex.Message}");
                }
            }

            return user;
        }

        // public bool AddUser(User user)
        // {
        //     using (SqlConnection connection = new SqlConnection(_connectionString))
        //     {
        //         string query = @"
        //             INSERT INTO users (username, password, email, full_name, created_at)
        //             VALUES (@Username, @Password, @Email, @FullName, @CreatedAt)";

        //         SqlCommand command = new SqlCommand(query, connection);
        //         command.Parameters.AddWithValue("@Username", user.Username);
        //         command.Parameters.AddWithValue("@Password", user.Password);
        //         command.Parameters.AddWithValue("@Email", user.Email);
        //         command.Parameters.AddWithValue("@FullName", user.FullName);
        //         command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

        //         try
        //         {
        //             connection.Open();
        //             int result = command.ExecuteNonQuery();
        //             return result > 0;
        //         }
        //         catch (Exception ex)
        //         {
        //             Logger.LogError($"Error adding user: {ex.Message}");
        //             return false;
        //         }
        //     }
        // }
        public bool AddUser(User user)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        string query = @"
            INSERT INTO users (username, password, email, full_name, created_at)
            VALUES (@Username, @Password, @Email, @FullName, @CreatedAt)";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Username", user.Username);
        command.Parameters.AddWithValue("@Password", user.Password);
        command.Parameters.AddWithValue("@Email", user.Email);
        command.Parameters.AddWithValue("@FullName", user.FullName);
        command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

        try
        {
            connection.Open();
            int result = command.ExecuteNonQuery();
            return result > 0;
        }
        catch (SqlException ex)
        {
            // Log more detailed SQL exception info
            Console.WriteLine($"SQL Error: {ex.Message}");
            Console.WriteLine($"Error Number: {ex.Number}");
            Console.WriteLine($"Error State: {ex.State}");
            Console.WriteLine($"Error LineNumber: {ex.LineNumber}");
            Console.WriteLine($"Error Procedure: {ex.Procedure}");
            Logger.LogError($"SQL Error adding user: {ex.Message} (Error Number: {ex.Number})");
            return false;
        }
        catch (Exception ex)
        {
            Logger.LogError($"Error adding user: {ex.Message}");
            return false;
        }
    }
}

        public bool UpdateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = @"
                    UPDATE users
                    SET username = @Username,
                        password = @Password,
                        email = @Email,
                        full_name = @FullName
                    WHERE id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@FullName", user.FullName);

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error updating user: {ex.Message}");
                    return false;
                }
            }
        }

        public bool DeleteUser(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM users WHERE id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    Logger.LogError($"Error deleting user: {ex.Message}");
                    return false;
                }
            }
        }
    }
}