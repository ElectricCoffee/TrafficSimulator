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
        int RoadWidth;

        public List<Sign> Signs = new List<Sign>();
        
        /// <Passing>
        /// Passing creates 3 basic coordinates that hopefully should allow the car to pass
        /// They all assume that there is space for passing
        /// The new route passes through a second road
        /// Seperate functions as otherwise they might save an old value
        /// Angle values may not fit correctly
        /// If spare time -> Device way to calculate in more than the basic directions
        /// </Passing>

        void PassingLeft(int CarWidth, int CarLength, Tuple<int, int> FrontCar, int Angle)
        {
            if (Angle == 90) //driving from left to right
            {
                Tuple<int, int> PassLeft = Tuple.Create(FrontCar.Item1 - CarLength/2, FrontCar.Item2 + CarWidth);
            }
            else if (Angle == 180) //
            {
                Tuple<int, int> PassLeft = Tuple.Create(FrontCar.Item1 + CarWidth, FrontCar.Item2 - CarLength/2);
            }
            else if (Angle == 270)
            {
                Tuple<int, int> PassLeft = Tuple.Create(FrontCar.Item1 + CarLength/2, FrontCar.Item2 - CarWidth);
            }
            else
            {
                Tuple<int, int> PassLeft = Tuple.Create(FrontCar.Item1 - CarWidth, FrontCar.Item2 + CarLength / 2);
            }
        }

        void PassingForward(int CarWidth, int CarLength, Tuple<int, int> FrontCar, int Angle)
        {
            if (Angle == 90)
            {
                Tuple<int, int> PassForward = Tuple.Create(FrontCar.Item1 + CarLength, FrontCar.Item2 + CarWidth);
            }
            else if (Angle == 180)
            {
                Tuple<int, int> PassForward = Tuple.Create(FrontCar.Item1 + CarWidth, FrontCar.Item2 + CarLength);
            }
            else if (Angle == 270)
            {
                Tuple<int, int> PassForward = Tuple.Create(FrontCar.Item1 - CarLength, FrontCar.Item2 - CarWidth);
            }
            else
            {
                Tuple<int, int> PassForward = Tuple.Create(FrontCar.Item1 - CarWidth, FrontCar.Item2 - CarLength);
            }
        }

        void PassingRight(int CarWidth, int CarLength, Tuple<int, int> FrontCar, int Angle)
        {
            if (Angle == 90)
            {
                Tuple<int, int> PassRight = Tuple.Create(FrontCar.Item1 + 2*CarLength, FrontCar.Item2);
            }
            else if (Angle == 180)
            {
                Tuple<int, int> PassRight = Tuple.Create(FrontCar.Item1, FrontCar.Item2 + 2*CarLength);
            }
            else if (Angle == 270)
            {
                Tuple<int, int> PassRight = Tuple.Create(FrontCar.Item1 - 2*CarLength, FrontCar.Item2);
            }
            else
            {
                Tuple<int, int> PassRight = Tuple.Create(FrontCar.Item1, FrontCar.Item2 + 2*CarLength);
            }
        }
    }
}
