using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectCSharp.Exceptions;
using CarConnectCSharp.Entity;

namespace CarConnectCSharp.Entity
{
    public class AuthenticationService
    {
        private DatabaseContext dbContext;

        public AuthenticationService()
        {
            dbContext = new DatabaseContext();
        }

        // Method to authenticate Admin
        public bool AuthenticateAdmin(string username, string password)
        {
            try
            {
                using (var connection = dbContext.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Admin WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Admin admin = new Admin
                                {
                                    AdminID = (int)reader["AdminID"],
                                    Username = (string)reader["Username"],
                                    Password = (string)reader["Password"]
                                };

                                return admin.Authenticate(password);
                            }
                            else
                            {
                                throw new AuthenticationException("Authentication of Admin is failed.");
                            }
                        }
                    }
                }
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            return false;
        }


        // Method to authenticate Customer
        public bool AuthenticateCustomer(string username, string password)
        {
            try
            {
                using (var connection = dbContext.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Customer WHERE Username = @Username";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Customer customer = new Customer
                                {
                                    CustomerID = (int)reader["CustomerID"],
                                    Username = (string)reader["Username"],
                                    Password = (string)reader["Password"]
                                };

                                return customer.Authenticate(password);
                            }
                            else
                            {
                                throw new AuthenticationException("Authentication of Customer is failed.");
                            }
                        }
                    }
                }
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            return false;
        }
    }
}

