using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnectCSharp.Exceptions
{
    class VehicleNotFoundException : Exception
    {
        public VehicleNotFoundException(string message) : base(message) { }

    }
}
