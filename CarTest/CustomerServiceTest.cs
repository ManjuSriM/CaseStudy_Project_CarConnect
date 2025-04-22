using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CarConnectCSharp.dao;
using CarConnectCSharp.Entity;

namespace CarTest
{
    class CustomerServiceTest
    {
        private CustomerService customerService;

        [SetUp]
        public void Setup()
        {
            customerService = new CustomerService();
        }

        [Test]
        public void TestUpdateCustomerInformation()
        {
            // Create a customer with valid existing Username
            Customer customer = new Customer
            {
                CustomerID = 1,
                Username = "miraj",
                FirstName = "MirajUpdated",
                LastName = "Khan",
                Email = "miraj_updated@example.com",
                PhoneNumber = "9998887776",
                Address = "New updated address",
                Password = "updatedPass123"
            };

            bool result = customerService.UpdateCustomer(customer);

            Assert.That(result, Is.True, "Customer information should be updated successfully");
        }

    }
}