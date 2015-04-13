using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    public class DescreteEvent : TrafficEvent
    {
        public DescreteEvent(TrafficCallback initCallbackMethod, TimeSpan initEventTime) : base(initCallbackMethod)
        {
            EventTime = initEventTime;
        }
        public TimeSpan EventTime { get; protected set; }
    }
}
