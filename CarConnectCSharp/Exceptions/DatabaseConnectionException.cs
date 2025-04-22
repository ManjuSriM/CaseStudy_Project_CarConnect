using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectCSharp.Exceptions
{
    class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException(string message) : base(message) { }

    }
}
