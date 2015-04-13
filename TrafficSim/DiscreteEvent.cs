using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    public class DiscreteEvent : TrafficEvent
    {
        public DiscreteEvent(TrafficCallback initCallbackMethod, TimeSpan initEventTime) : base(initCallbackMethod)
        {
            EventTime = initEventTime;
        }
        public TimeSpan EventTime { get; protected set; }
    }
}