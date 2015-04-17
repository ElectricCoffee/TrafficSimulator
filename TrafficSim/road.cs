using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrafficSim
{
    class road
    {
        /// <summary>
        /// the parameters
        /// might be better to convert it to a list
        /// this however is easier for others to access
        /// keep it like this for now, but ask others what they would prefer later
        /// </summary>
        /// <param name="StartX, StartY">start point of it</param>
        /// <param name="EndX, EndY">end point of it</param>
        /// <param name="RoadWidth">width of the road</param>
        int StartX, StartY;
        int EndX, EndY;
        int RoadWidth;

#warning sign is not yet implemented
        //public List<Sign> Signs = new List<Sign>();

        /// <summary>
        /// Each Passing function finds the coordinate required to pass a car in front of it
        /// It assumes there is enough room, and that there is only 1 car
        /// it is in 3 seperate functions, as otherwise it might save too old coordinates
        /// Should most likely not be used in road
        /// Keep it here until we find out were to put it
        /// </summary>
        /// <param name="CarWidth">self explanatory</param>
        /// <param name="CarLength">self explanatory</param>
        /// <param name="FrontCar">Coordinates for the car in front</param>
        /// <param name="Angle">the angle the car points at
        /// only takes north, south etc
        /// need to ensure the angle values are used the same way as in other classes</param>
        /// <param name="PassBool">Remind self to ask Andreas about this one</param>
        /// <returns></returns>

        [Obsolete("Use PassingHorizontal instead")]
        Point PassingLeft(int CarWidth, int CarLength, Point FrontCar, int Angle)
        {
            switch(Angle)
            {
                case 90: //Driving from left to right
                    return new Point(FrontCar.X - CarLength/2, FrontCar.Y + CarWidth);
                case 180: //Up to down
                    return new Point(FrontCar.X + CarWidth, FrontCar.Y - CarLength / 2);
                case 270: //Right to left
                    return new Point(FrontCar.X + CarLength / 2, FrontCar.Y - CarWidth);
                default: //Down to up
                    return new Point(FrontCar.X - CarWidth, FrontCar.Y + CarLength / 2);
            }
        }

        [Obsolete("Use PassingHorizontal instead")]
        Point PassingForward(int CarWidth, int CarLength, Point FrontCar, int Angle)
        {
            switch (Angle)
            {
                case 90: // Left to Right
                    return new Point(FrontCar.X + CarLength, FrontCar.Y + CarWidth);
                case 180: // Up to Down
                    return new Point(FrontCar.X + CarWidth, FrontCar.Y + CarLength);
                case 270: // Right to Left
                    return new Point(FrontCar.X - CarLength, FrontCar.Y - CarWidth);
                default: // Down to Up
                    return new Point(FrontCar.X - CarWidth, FrontCar.Y - CarLength);
            }
        }

        /// <summary>
        /// these are the optimized passing functions, made by Andreas.
        /// used for passing other cars see above summary
        /// Parameters see above
        /// </summary>
        private Point PassingHorizontal(int CarWidth, int CarLength, Point FrontCar, int Angle, bool PassRight)
        {
            switch (Angle)
            {
                case 90: // Driving from left to right
                    return PassRight ? new Point(FrontCar.X + 2 * CarLength, FrontCar.Y) : new Point(FrontCar.X - CarLength / 2, FrontCar.Y + CarWidth);
                case 180: // Up to down
                    return PassRight ? new Point(FrontCar.X, FrontCar.Y + 2 * CarLength) : new Point(FrontCar.X + CarWidth, FrontCar.Y - CarLength / 2);
                case 270: // Right to left
                    return PassRight ? new Point(FrontCar.X - 2 * CarLength, FrontCar.Y) : new Point(FrontCar.X + CarLength / 2, FrontCar.Y - CarWidth);
                default: // Down to up
                    return PassRight ? new Point(FrontCar.X, FrontCar.Y - 2 * CarLength) : new Point(FrontCar.X - CarWidth, FrontCar.Y + CarLength / 2);
            }
        }

        Point PassingRight(int CarWidth, int CarLength, Point FrontCar, int Angle)
        {
            switch (Angle)
            {
                case 90: // Left to Right
                    return new Point(FrontCar.X + 2 * CarLength, FrontCar.Y);
                case 180: // Up to Down
                    return new Point(FrontCar.X, FrontCar.Y + 2 * CarLength);
                case 270: // Right to Left
                    return new Point(FrontCar.X - 2 * CarLength, FrontCar.Y);
                default: // Down to Up
                    return new Point(FrontCar.X, FrontCar.Y - 2 * CarLength);
            }
        }
    }
}
