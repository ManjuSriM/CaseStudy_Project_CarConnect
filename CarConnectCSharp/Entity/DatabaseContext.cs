using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectCSharp.Entity
{
    class DatabaseContext
    {
        string connectionString = "Server=DESKTOP-C2IRK4B\\SQLSERVER2022;Database=CarConnect;Integrated Security=True;TrustServerCertificate=True";
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
