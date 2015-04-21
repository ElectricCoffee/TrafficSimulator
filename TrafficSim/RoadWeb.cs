using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TrafficSim;

namespace TrafficSim.RoadWeb
{
    class RoadWeb
    {
        public static RoadWeb operator +(RoadWeb lhs, Road rhs) { lhs.Add(rhs); return lhs; }
        public static RoadWeb operator +(RoadWeb lhs, Road[] rhs) { foreach (Road road in rhs) lhs.Add(road); return lhs; }
        public static RoadWeb operator -(RoadWeb lhs, Road rhs) { lhs.Remove(rhs); return lhs; }
        public static RoadWeb operator -(RoadWeb lhs, Road[] rhs) { foreach (Road road in rhs) lhs.Remove(road); return lhs; }

        private struct WebNode
        {
            public Intersection item;
            public List<WebNode> nextPossibles;
            public List<WebNode> lastPossibles;

            public WebNode(Intersection Item) 
            { 
                item = Item;
                nextPossibles = new List<WebNode>();
                lastPossibles = new List<WebNode>();
            }
        }

        private WebNode? head;
        private List<WebNode> completeRoadList;

        public RoadWeb(params Road[] roads)
        {
            foreach (Road road in roads) { Add(road); }
        }

        /* <summary>
         * Adds a road, and ties it into the network as needed.
         * </summary>
         * <param name="road">The road you'd like to add</param>
         */
        public void Add(Road road)
        {
            WebNode? start = null, end = null;

            for (int i = 0; i < completeRoadList.Count; i++)
            {
                if (completeRoadList[i].item.CenterPoint.Equals(road.StartPoint)) start = completeRoadList[i];
                else if (completeRoadList[i].item.CenterPoint.Equals(road.EndPoint)) end = completeRoadList[i];

                if (end != null && start != null) break;
            }

            if (start == null) 
            { 
                start = new WebNode(new Intersection(road));
                completeRoadList.Add(start.Value);
            }
            if (end == null) 
            { 
                end = new WebNode(new Intersection());
                completeRoadList.Add(end.Value);
            }

            start.Value.item.AddRoadChoice(road);
            start.Value.nextPossibles.Add(end.Value);
            end.Value.lastPossibles.Add(start.Value);

            if (head == null) { head = start; }
        }

        /*
         * <summary>
         * Adds a Road to the network, and can check if the road intersects with any other roads.
         * and in that case make a new intersection on that point
         * </summary>
         * <param name="road">The road to be added to the network</param>
         * <param name="CheckIfIntersect">if true, this will start a new thread that checks
         * if the newly added road intersects with any previous roads and make an intersection there.
         * If false, it'll just add the road, and ignore intersecting roads.</param>
         */
        public void Add(Road road, bool CheckIfIntersect)
        {
            Add(road);
            if (CheckIfIntersect)
            {
                ThreadStart start = delegate { IntersectPoint(road); };
                new Thread(start).Start();
            }
        }

        /* <summary>
         * This function deletes all traces and removes the road completly from the network
         * </summary>
         * <param name = "road">The road you'd like removed from the entire network</param>
         */
        public void Remove(Road road)
        {
            WebNode? toBeRemoved = null;

            foreach(WebNode node in completeRoadList)
            {
                if (node.item.Equals(road))
                {
                    toBeRemoved = node;
                    break;
                }
            }

            if (toBeRemoved == null) throw new ArgumentOutOfRangeException("Item does not exist in current Road network");

            foreach (WebNode node in toBeRemoved.value.lastPossibles) { node.nextPossibles.Remove(toBeRemoved); }
            foreach (WebNode node in toBeRemoved.value.nextPossibles) { node.lastPossibles.Remove(toBeRemoved); }
        }

#warning IntersectPoint & InsertIntersection are not yet completed
        private void IntersectPoint(Road road)
        {
            return;
        }
        private void InsertIntersection (Road roadOne, Road roadTwo, Tuple<int, int> IntersectPoint)
        {
            Road[] roadPartsPostSplit = new Road[4];

            
        }

        /*
         * Splits a road in two, and returns an array of the two new pieces
         */
        private Road[] SplitRoad(Road road, Tuple<int, int> splitPoint)
        {
            Road[] temp = new Road[2];

            temp[0] = new Road(splitPoint, road.EndPoint, road.RoadWidth);
            temp[1] = new Road(road.StartPoint, splitPoint, road.RoadWidth);

            return temp;
        }
    }

    class Intersection
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

        public Intersection(params Road[] exits)
        {
            CenterPoint = exits[0].StartPoint;
            intersectionExits = exits;
        }
        public Intersection();

        public void AddRoadChoice(Road road)
        {
            Road[] newRoads = new Road[intersectionExits.Length + 1];

            for (int i = 0; i < intersectionExits.Length; i++) { newRoads[i] = intersectionExits[i]; }
            newRoads[newRoads.Length - 1] = road;

            intersectionExits = newRoads;
        }
    }
}
