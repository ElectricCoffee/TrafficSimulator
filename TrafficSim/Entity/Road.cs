using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TrafficSim.Entity.Proxy;

namespace TrafficSim.Entity
{
    public class Road
    {
        private Intersection startPoint, endPoint;
        public Tuple<int, int> StartPoint
        {
            get { return startPoint.CenterPoint; }
            set { startPoint = new Intersection(value, this); }
        }
        public Tuple<int, int> EndPoint
        {
            get { return endPoint.CenterPoint; }
            set { startPoint = new Intersection(value, this); }
        }
        public Intersection StartIntersection
        {
            get { return startPoint; }
            set { startPoint = value; }
        }
        public Intersection EndIntersection
        {
            get { return endPoint; }
            set { endPoint = value; }
        }
#warning sign is not yet implemented
        public List<Sign> Signs { get; set; }

        public Road Next {get; set;}
        public int RoadWidth { get; set; }

        public Road() 
        {
             Signs = new List<Sign>();
        }

        public Road(Tuple<int, int> start, Tuple<int, int> end) : this()
        { 
            StartPoint = start; 
            EndPoint = end; 
            RoadWidth = 1; 
        }

        public Road(Tuple<int, int> start, Tuple<int, int> end, int width) : this(start, end)
        {
            RoadWidth = width;
        }

        public Road(Intersection start, Intersection end) : this()
        {
            startPoint = start;
            endPoint = end;
            RoadWidth = 1;
        }

        public Road(Intersection start, Intersection end, int width) : this(start, end)
        {
            RoadWidth = width;
        }

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
        /// <param name="PassRight">Wether or not to pass right around, if False, it'll pass left</param>
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
