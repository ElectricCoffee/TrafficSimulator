using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    class road
    {
        /// <variables>
        /// start and endpoint should be self explanatory
        /// roadsize is a placeholder for the width of the road
        /// the list should contain all signs used on the road
        /// </variables>
        Tuple<int, int> startPoint;
        Tuple<int, int> endPoint;
        int RoadSize;

        public List<Sign> Signs = new List<Sign>();
        
        /// <Passing>
        /// Passing creates 3 basic coordinates that hopefully should allow the car to pass
        /// needs better names and refinement in getting where the tuples point
        /// </Passing>
        void Passing(int CarLength, Tuple<int, int> frontCar)
        {
            Tuple<int, int> passLeft = Tuple.Create(frontCar.Item1 - (CarLength), frontCar.Item2 + RoadSize);
            Tuple<int, int> passForward = Tuple.Create(frontCar.Item1 + CarLength, passLeft.Item2);
            Tuple<int, int> passRight = Tuple.Create(frontCar.Item1 + (CarLength), frontCar.Item2); 
        }
    }
}
