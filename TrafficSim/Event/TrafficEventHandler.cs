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
        /// Handles callbacks for both discrete and continuous events. All continuous events are stored in a callback-list.
        /// </summary>
        /// <param name="ticklength">Duration between each callback of the continuous events</param>
        public TrafficEventHandler(TimeSpan ticklength)
        {
            TickLength = ticklength;
        }
        protected List<DiscreteEvent> discreteEventList = new List<DiscreteEvent>();
<<<<<<< HEAD
        protected Dictionary<Action, ContinuousEvent> continuousEventList = new Dictionary<Action, ContinuousEvent>();
=======
        protected List<ContinuousEvent> continuousEventList = new List<ContinuousEvent>();
>>>>>>> origin/master
        protected TimeSpan currentTime;
        /// <summary>
        /// Duration between each callback of the continuous events
        /// </summary>
        public TimeSpan TickLength {get; private set;}

        /// <summary>
        /// Queues up a callback to be called after a certain delay
        /// Should be used by ISimulatables
        /// </summary>
        /// <param name="callbackmethod">Method to be called</param>
        /// <param name="delay">Trigger delay</param>
        public void AddDiscreteEvent(Action callbackmethod, TimeSpan delay)
        {
            discreteEventList.Add(new DiscreteEvent(callbackmethod, currentTime + delay));
            discreteEventList = discreteEventList.OrderBy(TE => TE.EventTime).ToList();
        }

        /// <summary>
        /// Adds a callback to the callback-list
        /// Should be used by ISimulatables
        /// </summary>
        /// <param name="callbackmethod">Method to be added</param>
        public void AddContinuousEvent(Action callbackmethod)
        {
<<<<<<< HEAD
            continuousEventList.Add(callbackmethod, new ContinuousEvent(callbackmethod));
=======
            continuousEventList.Add(new ContinuousEvent(callbackmethod));
>>>>>>> origin/master
        }

        /// <summary>
        /// Removes the specified callback from the callback-list
        /// Should be used by ISimulatables
        /// </summary>
        /// <param name="callbackmethod">Method to be removed</param>
        public void RemoveContinuousEvent(Action callbackmethod)
        {
<<<<<<< HEAD
            if (continuousEventList.ContainsKey(callbackmethod)) continuousEventList.Remove(callbackmethod);
=======
            ContinuousEvent continuousEvent = continuousEventList.FirstOrDefault(x => x.Callback == callbackmethod);
            if (continuousEvent != null) continuousEventList.Remove(continuousEvent);
>>>>>>> origin/master
        }

        /// <summary>
        /// Clears all callbacks for the specified object from the callback-list
        /// Should be used by ISimulatables when removed from the simulation
        /// </summary>
        /// <param name="identity">The specified object</param>
        public void ClearEventsFromObject(ISimulatable identity)
        {
            continuousEventList = continuousEventList
<<<<<<< HEAD
                .Where(x => x.Key.Target != identity)
                .ToDictionary(x => x.Key, x => x.Value);
=======
                .Where(x => x.Callback.Target != identity)
                .ToList();
>>>>>>> origin/master
            discreteEventList = discreteEventList
                .Where(x => x.Callback.Target != identity)
                .ToList();
        }

        /// <summary>
        /// Clears the callback-list
        /// </summary>
        public void ClearContinuousEvents()
        {
            continuousEventList.Clear();
        }

        /// <summary>
        /// Clears all queued callbacks
        /// </summary>
        public void ClearDiscreteEvents()
        {
            discreteEventList.Clear();
        }

        /// <summary>
        /// Proceeds the simulation by one tick
        /// </summary>
        public void NextTick()
        {
            currentTime += TickLength;
<<<<<<< HEAD
            foreach (var kvp in continuousEventList)
=======
            foreach (var continuousEvent in continuousEventList)
>>>>>>> origin/master
            {
                continuousEvent.Callback();
            }
<<<<<<< HEAD
            if (discreteEventList.Count != 0 && discreteEventList[0].EventTime <= currentTime)
=======
            while (discreteEventList.Count != 0 && discreteEventList[0].EventTime <= currentTime)
>>>>>>> origin/master
            {
                discreteEventList[0].Callback();
                discreteEventList.RemoveAt(0);
            }
        }
    }
}
