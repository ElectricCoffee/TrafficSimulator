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
        private struct WebNode
        {
            public Intersection item;
            public List<WebNode> nextPossibles = new List<WebNode>();
            public List<WebNode> lastPossibles = new List<WebNode>();

            public WebNode(Intersection Item) { item = Item; }
        }

        private WebNode? head;
        private List<WebNode> completeRoadList;

#warning Add is not Completed
        public void Add(Road road)
        {
            WebNode? start = null, end = null;

            for (int i = 0; i < completeRoadList.Count; i++)
            {
                if (completeRoadList[i].item.CenterPoint.Equals(road.StartPoint)) start = completeRoadList[i];
                else if (completeRoadList[i].item.CenterPoint.Equals(road.EndPoint)) end = completeRoadList[i];

                if (end != null && start != null) break;
            }

            if (start == null) { start = new WebNode(new Intersection(road)); }
            if (end == null) { end = new WebNode(new Intersection()); }

            start.Value.item.AddRoadChoice(road);
            start.Value.nextPossibles.Add(end.Value);
            end.Value.lastPossibles.Add(start.Value);

            if (head == null) { head = start; }
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
