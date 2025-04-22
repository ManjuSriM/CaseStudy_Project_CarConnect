using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectCSharp.dao;
using CarConnectCSharp.Exceptions;
using CarConnectCSharp.Entity;

namespace CarConnectCSharp.dao
{
    public class AdminService : IAdminService
    {
        private DatabaseContext dbContext;

        public AdminService()
        {
            dbContext = new DatabaseContext();
        }

        public Admin GetAdminById(int adminId)
        {
            try
            {
                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open(); // Attempt to open the connection
                    string query = "SELECT * FROM Admin WHERE AdminID = @AdminID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@AdminID", adminId);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Admin
                        {
                            AdminID = (int)reader["AdminID"],
                            Username = reader["Username"].ToString()!,
                            Password = reader["Password"].ToString()!,
                            FirstName = reader["FirstName"].ToString()!,
                            LastName = reader["LastName"].ToString()!,
                            Email = reader["Email"].ToString()!,
                            PhoneNumber = reader["PhoneNumber"].ToString()!,
                            Role = reader["Role"].ToString()!,
                            JoinDate = (DateTime)reader["JoinDate"]
                        };
                    }
                    else
                    {
                        throw new AdminNotFoundException("Admin not found with the given ID.");
                    }
                }
            }
            catch (AdminNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return null!;
        }


        public Admin GetAdminByUsername(string username)
        {
            try
            {
                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open(); // Attempt to open the connection
                    string query = "SELECT * FROM Admin WHERE Username = @Username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Admin
                        {
                            AdminID = (int)reader["AdminID"],
                            Username = reader["Username"].ToString()!,
                            Password = reader["Password"].ToString()!,
                            FirstName = reader["FirstName"].ToString()!,
                            LastName = reader["LastName"].ToString()!,
                            Email = reader["Email"].ToString()!,
                            PhoneNumber = reader["PhoneNumber"].ToString()!,
                            Role = reader["Role"].ToString()!,
                            JoinDate = (DateTime)reader["JoinDate"]
                        };
                    }
                    else
                    {
                        throw new AdminNotFoundException("Admin not found with the given username.");
                    }
                }
            }
            catch (AdminNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return null!;
        }

        public bool RegisterAdmin(Admin adminData)
        {
            try
            {
                if (adminData == null)
                    throw new InvalidInputException("Admin data is null.");

                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open(); // Attempt to open the connection
                    string query = @"INSERT INTO Admin (AdminID, Username, Password, FirstName, LastName, Email, PhoneNumber, Role, JoinDate)
                             VALUES (@AdminID, @Username, @Password, @FirstName, @LastName, @Email, @PhoneNumber, @Role, @JoinDate)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@AdminID", adminData.AdminID);
                    command.Parameters.AddWithValue("@Username", adminData.Username);
                    command.Parameters.AddWithValue("@Password", adminData.Password);
                    command.Parameters.AddWithValue("@FirstName", adminData.FirstName);
                    command.Parameters.AddWithValue("@LastName", adminData.LastName);
                    command.Parameters.AddWithValue("@Email", adminData.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", adminData.PhoneNumber);
                    command.Parameters.AddWithValue("@Role", adminData.Role);
                    command.Parameters.AddWithValue("@JoinDate", adminData.JoinDate);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new InvalidInputException("Failed to register admin.");
                    return true;
                }
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            return false;
        }

        public bool UpdateAdmin(Admin adminData)
        {
            try
            {
                if (adminData == null)
                    throw new InvalidInputException("Admin data is null.");

                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open(); // Attempt to open the connection
                    string query = @"UPDATE Admin SET Username = @Username, Password = @Password, FirstName = @FirstName, LastName = @LastName, 
                            Email = @Email, PhoneNumber = @PhoneNumber, Role = @Role, JoinDate = @JoinDate
                            WHERE AdminID = @AdminID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@AdminID", adminData.AdminID);
                    command.Parameters.AddWithValue("@Username", adminData.Username);
                    command.Parameters.AddWithValue("@Password", adminData.Password);
                    command.Parameters.AddWithValue("@FirstName", adminData.FirstName);
                    command.Parameters.AddWithValue("@LastName", adminData.LastName);
                    command.Parameters.AddWithValue("@Email", adminData.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", adminData.PhoneNumber);
                    command.Parameters.AddWithValue("@Role", adminData.Role);
                    command.Parameters.AddWithValue("@JoinDate", adminData.JoinDate);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new AdminNotFoundException("Admin not found or no changes were made.");
                    return true;
                }
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (AdminNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;
        }

        public bool DeleteAdmin(int adminId)
        {
            try
            {
                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open(); // Attempt to open the connection
                    string query = "DELETE FROM Admin WHERE AdminID = @AdminID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@AdminID", adminId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new AdminNotFoundException("Admin not found or already deleted.");
                    return true;
                }
            }
            catch (AdminNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;
        }
    }
}

