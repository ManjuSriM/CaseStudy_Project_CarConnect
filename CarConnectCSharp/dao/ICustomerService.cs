using CarConnectCSharp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectCSharp.dao
{
    interface ICustomerService
    {
        Customer GetCustomerById(int customerId);
        Customer GetCustomerByUsername(string username);
        bool RegisterCustomer(Customer customerData);
        bool UpdateCustomer(Customer customerData);
        bool DeleteCustomer(int customerId);
    }
}
