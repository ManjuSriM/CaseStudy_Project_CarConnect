using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnectCSharp.Exceptions;

namespace CarConnectCSharp.Entity
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        private string? _email;
        public string? Email
        {
            get => _email;
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Contains("@"))
                {
                    _email = value;
                }
                else
                {
                    throw new InvalidInputException("Email must contain '@'.");
                }
            }
        }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime JoinDate { get; set; }

        public Customer() { }

        // Parameterized Constructor
        public Customer(int customerId, string firstName, string lastName, string email,
                        string phoneNumber, string address, string username,
                        string password, DateTime registrationDate)
        {
            CustomerID = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Username = username;
            Password = password;
            RegistrationDate = registrationDate;
        }

        // Method to authenticate user by password
        public bool Authenticate(string inputPassword)
        {
            return this.Password == inputPassword;
        }
    }
}

