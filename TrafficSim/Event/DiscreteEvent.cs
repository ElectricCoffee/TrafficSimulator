using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Event
{
    public class DiscreteEvent : TrafficEvent
    {
        public DiscreteEvent(Action initcallbackmethod, TimeSpan initeventtime) : base(initcallbackmethod)
        {
            EventTime = initeventtime;
        }
        public TimeSpan EventTime { get; protected set; }
    }
}