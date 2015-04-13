using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    public delegate void TrafficCallback();

    public abstract class TrafficEvent
    {
        public TrafficEvent(TrafficCallback initCallbackMethod)
        {
            Callback = initCallbackMethod;
        }
        public TrafficCallback Callback { get; protected set; }
    }
}
