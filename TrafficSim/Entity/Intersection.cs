using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficSim.Entity.Proxy;

namespace TrafficSim.Entity
{
    public class Intersection
    {
        public Tuple<int, int> CenterPoint { get; private set; }
        public bool IsSignalIntersection { get; private set; }

        private SignalLight signal;
        public SignalLight Signal
        {
            get { return signal; }
            set
            {
                signal = value;
                IsSignalIntersection = true;
            }
        }

        private Road[] intersectionExits;

        public Intersection(Tuple<int, int> Center, params Road[] roads)
        {
            CenterPoint = Center;
            intersectionExits = roads;
        }
        public Intersection() { }

        public void AddRoadChoice(Road road)
        {
            Road[] newRoads = new Road[intersectionExits.Length + 1];

            for (int i = 0; i < intersectionExits.Length; i++) { newRoads[i] = intersectionExits[i]; }
            newRoads[newRoads.Length - 1] = road;

            intersectionExits = newRoads;
        }

        public Road[] Options (Road current)
        {
            Road[] options = new Road[intersectionExits.Length - 1];

            bool foundCurrent = false;
            for (int i = 0; i < intersectionExits.Length; i++) 
            {
                options[foundCurrent ? i - 1 : i] = intersectionExits[i];
                foundCurrent = intersectionExits[i] == current;
            }

            return options;
        }
    }
}
