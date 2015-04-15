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
        int StartX, StartY;
        int EndX, EndY;
        int RoadWidth;

        public List<Sign> Signs = new List<Sign>();

        /// <summary>
        /// Each Passing function finds the coordinate required to pass a car in front of it
        /// It assumes there is enough room, and that there is only 1 car
        /// it is in 3 seperate functions, as otherwise it might save too old coordinates
        /// should also ensure that the coordinate is updated as the cars are moving
        /// </summary>
        /// <param name="CarWidth"></param>
        /// <param name="CarLength"></param>
        /// <param name="FrontCar"></param>
        /// <param name="Angle"></param>
        /// <param name="PassBool"></param>
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
        /// Er den Optimiserede PassForward & PassRight, Spørg Anders hvad den gør.
        /// Alt matematiken er stadig hans.
        /// </summary>
        /// <param name="CarWidth">Bilens Bredte</param>
        /// <param name="CarLength">Bilens Længde</param>
        /// <param name="FrontCar">Positionen på Bilen foran</param>
        /// <param name="Angle">hvilken Angel bilen er, i nærmeste 90 grader</param>
        /// <param name="PassRight">om bilen passerer højre eller venstre om</param>
        /// <returns></returns>
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
