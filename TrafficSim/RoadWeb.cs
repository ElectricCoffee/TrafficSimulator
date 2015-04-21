using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficSim;

namespace TrafficSim.RoadWeb
{
    class RoadWeb
    {
        public static void operator + (RoadWeb lhs, Road rhs) { lhs.Add(rhs); }
        public static void operator + (RoadWeb lhs, Road[] rhs) { foreach (Road road in rhs) lhs.Add(road); }
        public static void operator - (RoadWeb lhs, Road rhs) { lhs.Remove(rhs); }
        public static void operator - (RoadWeb lhs, Road[] rhs) { foreach (Road road in rhs) lhs.Remove(road); }

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
