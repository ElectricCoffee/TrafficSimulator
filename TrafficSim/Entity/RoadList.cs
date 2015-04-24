using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Entity
{
    /// <summary>
    /// The linked list with the roads in it
    /// Access the next element with Head.Next
    /// </summary>
    public class RoadList
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

}
