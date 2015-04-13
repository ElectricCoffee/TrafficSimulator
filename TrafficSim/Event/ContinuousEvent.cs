using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Event
{
    public class ContinuousEvent : TrafficEvent
    {
        public ContinuousEvent(TrafficCallback initCallback) : base(initCallback) { }
    }
}
