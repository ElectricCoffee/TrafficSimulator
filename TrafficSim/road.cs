using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrafficSim
{
    /// <summary>
    /// The linked list with the roads in it
    /// Access the next element with Head.Next
    /// </summary>
    class RoadList
    {
        private Road Head;
        private Road Current;

        /// <summary>
        /// Sets Current to be the next road
        /// Head always stays the first road
        /// might have to ensure caller understands if current.next == null
        /// </summary>
        public void NextRoad()
        {
            if (Current.Next != null) 
                Current.Next = Head;
        }

        /// <summary>
        /// Add a new road
        /// Always puts it at the end
        /// If there is no Head it puts the new one in Head
        /// </summary>
        /// <param name="Start">Starting point of the new road</param>
        /// <param name="End">Ending point of the new road</param>
        /// <param name="RoadWidth">width of the road</param>
        public void Add(Tuple<int, int> Start, Tuple<int, int> End, int RoadWidth)
        {
            if (Head == null)
            {
                Head = new Road();
                Current = Head;

                Head.StartPoint = Start;
                Head.EndPoint = End;
                Head.RoadWidth = RoadWidth;
                Head.Next = null;
            }
            else
            {
                Road ToAdd = new Road();

                ToAdd.StartPoint = Start;
                ToAdd.EndPoint = End;
                ToAdd.RoadWidth = RoadWidth;

                Road Checking = Head;

                while (Checking.Next != null)
                {
                    Checking = Checking.Next;
                }

                Checking.Next = ToAdd;
            }
        }
    }

    class Road
    {
        public Road Next;
        public Tuple<int, int> StartPoint;
        public Tuple<int, int> EndPoint;
        public int RoadWidth;

        public Road();
        public Road(Tuple<int, int> start, Tuple<int, int> end)
        { 
            StartPoint = start; 
            EndPoint = end; 
            RoadWidth = 1; 
        }
        public Road(Tuple<int, int> start, Tuple<int, int> end, int width) 
        { 
            StartPoint = start; 
            EndPoint = end; 
            RoadWidth = width; 
        }

#warning sign is not yet implemented
        List<Sign> Signs = new List<Sign>();

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
