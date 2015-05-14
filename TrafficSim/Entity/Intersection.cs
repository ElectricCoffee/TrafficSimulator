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

        public List<Road> IntersectionExits { get; private set; }

        public Intersection(Tuple<int, int> Center, params Road[] roads)
        {
            CenterPoint = Center;
            IntersectionExits = new List<Road>(roads);
        }
        public Intersection() { }

        public void AddRoadChoice(Road road)
        {
            IntersectionExits.Add(road);
        }

        public Road[] Options (Road current)
        {
            Road[] options = new Road[IntersectionExits.Count - 1];

            bool foundCurrent = false;
            for (int i = 0; i < IntersectionExits.Count; i++) 
            {
                options[foundCurrent ? i - 1 : i] = IntersectionExits[i];
                foundCurrent = IntersectionExits[i] == current;
            }

            return options;
        }

        public void ReplaceOption(Road toBeReplaced, Road replacement)
        {
            for (int i = 0; i < IntersectionExits.Count; i++)
            {
                if (IntersectionExits[i].Equals(toBeReplaced)) IntersectionExits[i] = replacement;
            }
        }
    }
}
