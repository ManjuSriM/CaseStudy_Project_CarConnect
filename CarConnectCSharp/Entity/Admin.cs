using CarConnectCSharp.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectCSharp.Entity
{
    public class Admin
    {
        public int AdminID { get; set; }
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
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
        public DateTime JoinDate { get; set; }


        public bool Authenticate(string password)
        {
            return Password == password;
        }
    }
}

