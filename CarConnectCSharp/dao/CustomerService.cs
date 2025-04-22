using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectCSharp.Exceptions;
using CarConnectCSharp.dao;
using CarConnectCSharp.Entity;

namespace CarConnectCSharp.dao
{
    public class CustomerService : ICustomerService
    {
        private DatabaseContext dbContext;

        public CustomerService()
        {
            dbContext = new DatabaseContext();
        }

        public Customer GetCustomerById(int customerId)
        {
            try
            {
                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open(); // Attempt to open the connection
                    string query = "SELECT * FROM Customer WHERE CustomerID = @CustomerID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerID", customerId);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = reader["FirstName"].ToString()!,
                            LastName = reader["LastName"].ToString()!,
                            Email = reader["Email"].ToString()!,
                            PhoneNumber = reader["PhoneNumber"].ToString()!,
                            Username = reader["Username"].ToString()!,
                            Address = reader["Address"].ToString()!,
                            RegistrationDate = (DateTime)reader["RegistrationDate"]
                        };
                    }
                    else
                    {
                        throw new CustomerNotFoundException("Customer not found with the given ID.");
                    }
                }
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return null!;
        }

        public Customer GetCustomerByUsername(string username)
        {
            try
            {
                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open(); // Attempt to open the connection
                    string query = "SELECT * FROM Customer WHERE Username = @Username";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", username);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = reader["FirstName"].ToString()!,
                            LastName = reader["LastName"].ToString()!,
                            Email = reader["Email"].ToString()!,
                            PhoneNumber = reader["PhoneNumber"].ToString()!,
                            Username = reader["Username"].ToString()!,
                            Address = reader["Address"].ToString()!,
                            RegistrationDate = (DateTime)reader["RegistrationDate"]
                        };
                    }
                    else
                    {
                        throw new CustomerNotFoundException("Customer not found with the given username.");
                    }
                }
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return null!;
        }

        public bool RegisterCustomer(Customer customerData)
        {
            try
            {
                if (customerData == null)
                    throw new InvalidInputException("Customer data is null.");

                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open(); // Attempt to open the connection
                    string query = @"INSERT INTO Customer (CustomerID, FirstName, LastName, Email, PhoneNumber, Username, Password, Address, RegistrationDate)
                             VALUES (@CustomerID, @FirstName, @LastName, @Email, @PhoneNumber, @Username, @Password, @Address, @RegistrationDate)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerID", customerData.CustomerID);
                    command.Parameters.AddWithValue("@FirstName", customerData.FirstName);
                    command.Parameters.AddWithValue("@LastName", customerData.LastName);
                    command.Parameters.AddWithValue("@Email", customerData.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", customerData.PhoneNumber);
                    command.Parameters.AddWithValue("@Username", customerData.Username);
                    command.Parameters.AddWithValue("@Password", customerData.Password);
                    command.Parameters.AddWithValue("@Address", customerData.Address);
                    command.Parameters.AddWithValue("@RegistrationDate", customerData.RegistrationDate);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new InvalidInputException("Failed to register customer.");
                    return true;
                }
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;
        }

        public bool UpdateCustomer(Customer customerData)
        {
            try
            {
                if (customerData == null)
                    throw new InvalidInputException("Customer data is null.");

                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open(); // Attempt to open the connection
                    string query = @"UPDATE Customer SET FirstName = @FirstName, LastName = @LastName, Email = @Email, 
                                 PhoneNumber = @PhoneNumber, Address = @Address WHERE CustomerID = @CustomerID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerID", customerData.CustomerID);
                    command.Parameters.AddWithValue("@FirstName", customerData.FirstName);
                    command.Parameters.AddWithValue("@LastName", customerData.LastName);
                    command.Parameters.AddWithValue("@Email", customerData.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", customerData.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", customerData.Address);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new CustomerNotFoundException("Customer not found or no changes were made.");
                    return true;
                }
            }
            catch (InvalidInputException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;
        }

        public bool DeleteCustomer(int customerId)
        {
            try
            {
                using (SqlConnection connection = dbContext.GetConnection())
                {
                    connection.Open(); // Attempt to open the connection
                    string query = "DELETE FROM Customer WHERE CustomerID = @CustomerID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerID", customerId);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                        throw new CustomerNotFoundException("Customer not found or already deleted.");
                    return true;
                }
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return false;
        }
    }
}