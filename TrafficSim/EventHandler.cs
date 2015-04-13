using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim {

    public class EventHandler {

        protected List<TrafficEvent> DescreteEventList = new List<TrafficEvent>();

        protected TimeSpan currentTime;
        /// <summary>
        /// Queues up a method to be called after a certain delay.
        /// </summary>
        /// <param name="callbackMethod">Method to be called</param>
        /// <param name="delay">Delay in seconds</param>
        public void AddEvent(TrafficCallback callbackMethod, TimeSpan delay) {
            DescreteEventList.Add(new TrafficEvent(callbackMethod, currentTime + delay));
            DescreteEventList = DescreteEventList.OrderBy(TE => TE.EventTime).ToList();
        }
    }
}
