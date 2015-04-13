using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    public delegate void TrafficCallback();
    public class DescreteEvent
    {
        public DescreteEvent(TrafficCallback initCallbackMethod, TimeSpan initEventTime)
        {
            Callback = initCallbackMethod;
            EventTime = initEventTime;
        }
        public TrafficCallback Callback { get; protected set; }
        public TimeSpan EventTime { get; protected set; }
    }
}
