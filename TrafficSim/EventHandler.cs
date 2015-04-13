using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    public class EventHandler
    {
        protected List<DescreteEvent> DescreteEventList = new List<DescreteEvent>();

        protected TimeSpan currentTime;
        /// <summary>
        /// Queues up a method to be called after a certain delay.
        /// </summary>
        /// <param name="callbackMethod">Method to be called</param>
        /// <param name="delay">Trigger delay</param>
        public void AddDescreteEvent(TrafficCallback callbackMethod, TimeSpan delay)
        {
            DescreteEventList.Add(new DescreteEvent(callbackMethod, currentTime + delay));
            DescreteEventList = DescreteEventList.OrderBy(TE => TE.EventTime).ToList();
        }
    }
}
