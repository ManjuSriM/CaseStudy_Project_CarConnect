using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CarConnectCSharp.Entity;

namespace CarTest
{
    class CustomerTest
    {
        private AuthenticationService _authService;

        [SetUp]
        public void Setup()
        {
            //var dbContext = new DatabaseContext("Your_Connection_String_Here");
            _authService = new AuthenticationService();
        }

        [Test]
        public void AuthenticateCustomer_InvalidCredentials_ReturnsFalse()
        {
            // Arrange
            string username = "miraj";
            string password = "wrongpass";

            // Act
            bool result = _authService.AuthenticateCustomer(username, password);

            // Assert
            Assert.That(result, Is.False, "Authentication failed : Invalid credentials.");
        }
    }
}