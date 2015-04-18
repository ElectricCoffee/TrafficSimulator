using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Util
{
    class NoDirectionException : Exception
    {
        public NoDirectionException() : base("You supplied a (0,0) direction, this is not allowed, as it can cause issues down the line.") { }

        public NoDirectionException(string message) : base(message) { }

        public NoDirectionException(string message, Exception inner) : base(message, inner) { }
    }
}
