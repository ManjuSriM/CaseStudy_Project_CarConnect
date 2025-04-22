using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectCSharp.Exceptions
{
    class AdminNotFoundException : Exception
    {
        public AdminNotFoundException(string message) : base(message) { }

    }
}
