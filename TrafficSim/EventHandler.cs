using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim {
    public class EventHandler {

        protected List<TrafficEvent> TrafficEventList = new List<TrafficEvent>();

        protected TimeSpan currentTime;

        public void AddEvent(TrafficCallback callbackMethod, TimeSpan delay) {
            TrafficEventList.Add(new TrafficEvent(callbackMethod, currentTime + delay));
            TrafficEventList = TrafficEventList.OrderBy(TE => TE.EventTime).ToList();
        }
    }
}
