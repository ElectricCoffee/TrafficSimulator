using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficSim.Util.Collections;

namespace TrafficSim.Event
{
    public class TrafficEventHandler
    {
        /// <summary>
        /// Handles callbacks for both discrete and continuous events. All contiuous events are stored in a callback-list.
        /// </summary>
        /// <param name="initTickLength">Duration between each callback of the continuous events</param>
        public TrafficEventHandler(TimeSpan initTickLength)
        {
            tickLength = initTickLength;
        }
        protected List<DiscreteEvent> DiscreteEventList = new List<DiscreteEvent>();
        protected Dictionary<Action, ContinuousEvent> ContinuousEventList = new Dictionary<Action, ContinuousEvent>();
        protected TimeSpan currentTime;
        /// <summary>
        /// Duration between each callback of the continuous events
        /// </summary>
        public TimeSpan tickLength {get; private set;}

        /// <summary>
        /// Queues up a callback to be called after a certain delay
        /// </summary>
        /// <param name="callbackMethod">Method to be called</param>
        /// <param name="delay">Trigger delay</param>
        public void AddDiscreteEvent(Action callbackMethod, TimeSpan delay)
        {
            DiscreteEventList.Add(new DiscreteEvent(callbackMethod, currentTime + delay));
            DiscreteEventList = DiscreteEventList.OrderBy(TE => TE.EventTime).ToList();
        }

        /// <summary>
        /// Adds a callback to the callback-list
        /// </summary>
        /// <param name="callbackMethod">Method to be added</param>
        public void AddContinuousEvent(Action callbackMethod)
        {
            ContinuousEventList.Add(callbackMethod, new ContinuousEvent(callbackMethod));
        }

        /// <summary>
        /// Removes the specified callback from the callback-list
        /// </summary>
        /// <param name="callbackMethod">Method to be removed</param>
        public void RemoveContinuousEvent(Action callbackMethod)
        {
            if (ContinuousEventList.ContainsKey(callbackMethod)) ContinuousEventList.Remove(callbackMethod);
        }

        /// <summary>
        /// Clears all callbacks for the specified object from the callback-list
        /// </summary>
        /// <param name="identity">The specified object</param>
        public void ClearEventsFromObject(object identity)
        {
            foreach (var kvp in ContinuousEventList)
            {
                if (kvp.Key.Target == identity) ContinuousEventList.Remove(kvp.Key);
            }
        }

        /// <summary>
        /// Clears the callback-list
        /// </summary>
        public void ClearContinuousEvents()
        {
            ContinuousEventList.Clear();
        }

        /// <summary>
        /// Proceeds the simulation by one tick
        /// </summary>
        public void NextTick()
        {
            currentTime += tickLength;
            foreach (var kvp in ContinuousEventList)
            {
                kvp.Value.Callback();
            }
            if (DiscreteEventList.Count != 0 && DiscreteEventList[0].EventTime <= currentTime)
            {
                DiscreteEventList[0].Callback();
                DiscreteEventList.RemoveAt(0);
            }
        }

        public void AddDiscreteEvent()
        {
            throw new NotImplementedException();
        }
    }
}
