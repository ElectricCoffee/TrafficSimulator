using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Event
{
    public delegate void TrafficCallback();

    public abstract class TrafficEvent
    {
        public TrafficEvent(TrafficCallback initCallback)
        {
            Callback = initCallback;
        }

        public virtual TrafficCallback Callback { get; protected set; }
    }
}
