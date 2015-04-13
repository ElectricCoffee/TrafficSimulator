﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    public class EventHandler
    {
        protected List<DiscreteEvent> DiscreteEventList = new List<DiscreteEvent>();
        protected Dictionary<TrafficCallback, ContinuousEvent> ContinuousEventList = new Dictionary<TrafficCallback, ContinuousEvent>();
        protected TimeSpan currentTime;

        /// <summary>
        /// Queues up a callback to be called after a certain delay.
        /// </summary>
        /// <param name="callbackMethod">Method to be called</param>
        /// <param name="delay">Trigger delay</param>
        public void AddDiscreteEvent(TrafficCallback callbackMethod, TimeSpan delay)
        {
            DiscreteEventList.Add(new DiscreteEvent(callbackMethod, currentTime + delay));
            DiscreteEventList = DiscreteEventList.OrderBy(TE => TE.EventTime).ToList();
        }

        /// <summary>
        /// Adds a callback to the callback-list; called each tick.
        /// </summary>
        /// <param name="callbackMethod">Method to be added</param>
        public void AddContinuousEvent(TrafficCallback callbackMethod)
        {
            ContinuousEventList.Add(callbackMethod, new ContinuousEvent(callbackMethod));
        }

        /// <summary>
        /// Removes the specified callback from the callback-list.
        /// </summary>
        /// <param name="callbackMethod">Method to be removed</param>
        public void RemoveContinuousEvent(TrafficCallback callbackMethod)
        {
            if (ContinuousEventList.ContainsKey(callbackMethod)) ContinuousEventList.Remove(callbackMethod);
        }

        /// <summary>
        /// Clears all callbacks for the specified object from the callback-list.
        /// </summary>
        /// <param name="identity">The specified object</param>
        public void ClearContinuousEvents(object identity)
        {
            foreach (var kvp in ContinuousEventList)
            {
                if (kvp.Key.Target == identity) ContinuousEventList.Remove(kvp.Key);
            }
        }
    }
}
