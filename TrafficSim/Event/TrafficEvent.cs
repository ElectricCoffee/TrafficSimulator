using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Event
{
    public abstract class TrafficEvent
    {
        public TrafficEvent(Action initcallback)
        {
            Callback = initcallback;
        }

        public virtual Action Callback { get; protected set; }
    }
}
