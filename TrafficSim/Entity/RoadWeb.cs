using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using TrafficSim;
using TrafficSim.Entity.Proxy;

namespace TrafficSim.Entity
{
    public class RoadWeb
    {
        /*How to use this class
         * Create a road with CreateRoad
         * When you get to the end of the road get the next road with road.Next
         * if there is a intersection use IntersectTurn to get the next road
         *   as of yet, it requires the Angle of the road you want to turn to
         * The complete list of roads is contained in a combination of .Next and intersections
         *   to get them: forEach(Webnode node in completeRoadList) forEach(Road road in node.Item.IntersectionExits)
         *     Road CheckingRoad = road do { bla; CheckingRoad = CheckingRoad.Next} while(CheckingRoad != null)
         *   
         */
        public static RoadWeb operator +(RoadWeb lhs, Road rhs) { lhs.Add(rhs); return lhs; }
        public static RoadWeb operator +(RoadWeb lhs, Road[] rhs) { foreach (Road road in rhs) lhs.Add(road); return lhs; }
        public static RoadWeb operator -(RoadWeb lhs, Road rhs) { lhs.Remove(rhs); return lhs; }
        public static RoadWeb operator -(RoadWeb lhs, Road[] rhs) { foreach (Road road in rhs) lhs.Remove(road); return lhs; }

        private class WebNode
        {
            public Intersection Item { get; set; }
            public List<WebNode> NextPossibles { get; set; }
            public List<WebNode> LastPossibles { get; set; }

            public WebNode(Intersection item)
            {
                Item = item;
                NextPossibles = new List<WebNode>();
                LastPossibles = new List<WebNode>();
            }
        }

        private WebNode head;
        private List<WebNode> completeRoadList;
        private List <Point> SpawnPoint;
        private List <Point> DeSpawnPoint;

        public void CreateSpawnPoint(Point ThePoint)
        {
            SpawnPoint.Add(ThePoint);
        }

        public void CreateDeSpawnPoint(Point ThePoint)
        {
            DeSpawnPoint.Add(ThePoint);
        }

        /// <summary>
        /// Adds Several roads into the network
        /// Requires pre-made roads, if you don't have that use CreateRoad
        /// </summary>
        /// <param name="roads"></param>
        public RoadWeb(params Road[] roads)
        {
            foreach (Road road in roads) { Add(road); }
        }

        /// <summary>
        /// Creates a new road and adds it via the add function
        /// cannot figure out whether it is connected to a road by itself
        /// leave start null to link it to the road you give it
        /// leave road null to either find the road it connects to or not connect it to anything
        /// </summary>
        /// <param name="Start">the startpoint, null it if linking with road</param>
        /// <param name="End">the end point of the road</param>
        /// <param name="Angle">the angle of the road (for when the road turns)</param>
        /// <param name="Width">The width of the road</param>
        /// <param name="road">The road it should be linked with, leave null if it is a seperate road</param>
        public void CreateRoad(Point Start, Point End, int Angle, int Width, Road road)
        {
            Road NewRoad = new Road();
            NewRoad.EndPoint = End;
            NewRoad.Angle = Angle;
            NewRoad.RoadWidth = Width;

            if (Start == null)
            {
                NewRoad.StartPoint = road.EndPoint;
                road.Next = NewRoad;
            }
            else
            {
                NewRoad.StartPoint = Start;
                road = FindConnectingRoad(NewRoad);
                if (road != null)
                    road.Next = NewRoad;
            }

            Add(NewRoad, true);
        }

        /// <summary>
        /// finds the road the input is connected to
        /// compares the inputs StartPoint to the checkings endpoint
        /// </summary>
        /// <param name="road">the road to check if it is connected to anything</param>
        /// <returns>the road it is connected to</returns>
        public Road FindConnectingRoad(Road road)
        {
            foreach (WebNode node in completeRoadList)
                foreach (Road TempRoad in node.Item.IntersectionExits)
                {
                    Road CheckingRoad = TempRoad;
                    do{
                        if (road.StartPoint == CheckingRoad.EndPoint)
                            return CheckingRoad;

                        CheckingRoad = CheckingRoad.Next;
                    } while(CheckingRoad != null);
                }
            return null;
        }

        /// <summary>
        /// Adds a new road to the Network
        /// always adds them into an intersection
        /// </summary>
        /// <param name="road">the road that should be put into the network</param>
        public void Add(Road road)
        {
            WebNode start = null, end = null;

            for (int i = 0; i < completeRoadList.Count; i++)
            {
                if (completeRoadList[i].Item.CenterPoint.Equals(road.StartPoint)) start = completeRoadList[i];
                else if (completeRoadList[i].Item.CenterPoint.Equals(road.EndPoint)) end = completeRoadList[i];

                if (end != null && start != null) break;
            }

            if (start == null)
            {
                start = new WebNode(road.StartIntersection);
                completeRoadList.Add(start);
            }
            if (end == null)
            {
                end = new WebNode(new Intersection());
                completeRoadList.Add(end);
            }

            start.Item.AddRoadChoice(road);
            start.NextPossibles.Add(end);
            end.LastPossibles.Add(start);

            head = head ?? start;
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
                new Thread(() => IntersectPoint(road)).Start();
        }

        /* <summary>
         * This function deletes all traces and removes the road completly from the network
         * </summary>
         * <param name = "road">The road you'd like removed from the entire network</param>
         */
        public void Remove(Road road)
        {
            WebNode toBeRemoved = null;

            foreach (WebNode node in completeRoadList)
            {
                if (node.Item.Equals(road))
                {
                    toBeRemoved = node;
                    break;
                }
            }

            if (toBeRemoved == null) throw new ArgumentOutOfRangeException("Item does not exist in current Road network");

            foreach (WebNode node in toBeRemoved.LastPossibles) { node.NextPossibles.Remove(toBeRemoved); }
            foreach (WebNode node in toBeRemoved.NextPossibles) { node.LastPossibles.Remove(toBeRemoved); }
        }

        private WebNode GetWebNode(Intersection intersection)
        {
            foreach (WebNode wn in completeRoadList) { if (wn.Item.Equals(intersection)) return wn; }
            throw new ArgumentOutOfRangeException("{0} does not exist in the current list", intersection.ToString());
        }

        /// <summary>
        /// Returns the road you turn to in a intersection
        /// Later development should change it to left-right forwards turning
        /// </summary>
        /// <param name="StartRoad">The road you come from</param>
        /// <param name="Direction">The angle of the road you turn to</param>
        /// <returns></returns>
        private Road IntersectTurn(Road StartRoad, int Direction)
        {
            foreach (Road road in head.Item.IntersectionExits)
            {
                if (road.Angle == Direction)
                {
                    return road;
                }
            }
            return null;
        }
        /// <summary>
        /// Checks if the inputted road intersects with any roads already part of the roadweb
        /// Assumes all the roads can be put into either y = ax+b or y = b
        /// </summary>
        /// <param name="FirstRoad">The Road that possibly might intersect</param>
        /// <returns></returns>
        private Point IntersectPoint(Road FirstRoad)
        {
            foreach (WebNode node in completeRoadList)
                foreach (Road CheckingRoad in node.Item.IntersectionExits)
                {
                    Road SecondRoad = CheckingRoad;
                    do {
                         //this is to ensure a float division instead of an integer division
                        float FirstStartItem1Calc = FirstRoad.StartPoint.X;
                        float FirstEndItem1Calc = FirstRoad.EndPoint.X;
                        float FirstStartItem2Calc = FirstRoad.StartPoint.Y;
                        float FirstEndItem2Calc = FirstRoad.EndPoint.Y;

                    //it's the a in y = ax + b
                        float FirstSlope = (FirstStartItem2Calc - FirstEndItem2Calc) /
                                           (FirstStartItem1Calc - FirstEndItem1Calc);

                        float SecondStartItem1Calc = SecondRoad.StartPoint.X;
                        float SecondEndItem1Calc = SecondRoad.EndPoint.X;
                        float SecondStartItem2Calc = SecondRoad.StartPoint.Y;
                        float SecondEndItem2Calc = SecondRoad.EndPoint.Y;

                        float SecondSlope = (SecondStartItem2Calc - SecondEndItem2Calc) /
                                            (SecondStartItem1Calc - SecondEndItem1Calc);

                    //it's the b in y = ax+b
                        float FirstStart = -1 * (FirstSlope * FirstStartItem1Calc - FirstStartItem2Calc);

                        float SecondStart = -1 * (SecondSlope * SecondStartItem1Calc - SecondStartItem2Calc);
 
                    //this is in the cases where a might end up infinite
                    //checks seperately for each road - possible to optimize?
                        if (FirstStartItem1Calc - FirstEndItem1Calc == 0)
                        {
                            if (SecondStart >= (FirstStartItem2Calc > FirstEndItem2Calc ? FirstStartItem2Calc : FirstEndItem2Calc) || (
                                SecondStart <= (FirstStartItem2Calc < FirstEndItem2Calc ? FirstStartItem2Calc : FirstEndItem2Calc))
                             )
                                return new Point(null, null);

                            else{
                                int CrossingPointX = FirstRoad.StartPoint.X;
                                int CrossingPointY = (int)SecondSlope * SecondRoad.StartPoint.X + (int)SecondStart;
                                InsertIntersection(FirstRoad, SecondRoad, new Point ((int)CrossingPointX, (int)CrossingPointY));
                                return new Point((int)CrossingPointX, (int)CrossingPointY);
                            }
                        }
                        else if (SecondStartItem1Calc - SecondStartItem1Calc == 0)
                        {
                            if (FirstStart >= (SecondStartItem2Calc > SecondEndItem2Calc ? SecondStartItem2Calc : SecondEndItem2Calc) || (
                                FirstStart <= (SecondStartItem2Calc < SecondEndItem2Calc ? SecondStartItem2Calc : SecondEndItem2Calc))
                              )
                                return new Point(null, null);
                            else
                            {
                                int CrossingPointX = SecondRoad.StartPoint.X;
                                int CrossingPointY = (int)FirstSlope * FirstRoad.StartPoint.X + (int)FirstStart;
                                InsertIntersection(FirstRoad, SecondRoad, new Point((int)CrossingPointX, (int)CrossingPointY));
                                return new Point((int)CrossingPointX, (int)CrossingPointY);
                            }
                        }
                        //assumes that if the roads have the same growth then they won't intersect
                        if (FirstSlope != SecondSlope)
                        {
                            //the points that they cross
                            //assumes that at some point they will cross
                            //only takes into consideration the lines equation y = ax+b
                            float CrossingPointY = (SecondSlope * -FirstStart + FirstSlope * SecondStart) / (FirstSlope - SecondSlope);
                            float CrossingPointX = (CrossingPointY - SecondStart) / SecondSlope;

                            //checks whether it crosses at a point within the line or not
                            if ((
                                 CrossingPointX >= (FirstRoad.StartPoint.X > FirstRoad.EndPoint.X ?
                                                    FirstRoad.StartPoint.X : FirstRoad.EndPoint.X) || (
                                 CrossingPointX >= (SecondRoad.StartPoint.X > SecondRoad.EndPoint.X ?
                                                    SecondRoad.StartPoint.X : SecondRoad.EndPoint.X)) || (
                                 CrossingPointX <= (FirstRoad.StartPoint.X < FirstRoad.EndPoint.X ?
                                                    FirstRoad.StartPoint.X : FirstRoad.EndPoint.X)) ||
                                 CrossingPointX <= (SecondRoad.StartPoint.X < SecondRoad.EndPoint.X ?
                                                    SecondRoad.StartPoint.X : SecondRoad.EndPoint.X)) && (
                                 CrossingPointY >= (FirstRoad.StartPoint.Y > FirstRoad.EndPoint.Y ?
                                                    FirstRoad.StartPoint.Y : FirstRoad.EndPoint.Y)) || (
                                 CrossingPointY >= (SecondRoad.StartPoint.Y > SecondRoad.EndPoint.Y ?
                                                    SecondRoad.StartPoint.Y : SecondRoad.EndPoint.Y)) || (
                                 CrossingPointY <= (FirstRoad.StartPoint.Y < FirstRoad.EndPoint.Y ?
                                                    FirstRoad.StartPoint.Y : FirstRoad.EndPoint.Y)) ||
                                 CrossingPointY <= (SecondRoad.StartPoint.Y < SecondRoad.EndPoint.Y ?
                                                    SecondRoad.StartPoint.Y : SecondRoad.EndPoint.Y)
                               )
                            {
                            }
                            else 
                            {
                                //This one's for when they cross, the other's for when they don't
                               InsertIntersection(FirstRoad, SecondRoad, new Point((int)CrossingPointX, (int)CrossingPointY));
                                return new Point((int)CrossingPointX, (int)CrossingPointY);
                            }
                        }
                        SecondRoad = SecondRoad.Next;
                        } while(SecondRoad != null);
                    }
                return new Point(null, null);
            }

        private void InsertIntersection(Road roadOne, Road roadTwo, Point IntersectPoint)
        {
            Road[] roadPartsPostSplit = new Road[4];
            WebNode[] wns = new WebNode[4];

            for (int i = 0; i < 4; i++)
            {
                roadPartsPostSplit[i] = (i >= 2) ?
                    SplitRoad(roadOne, IntersectPoint)[i] : SplitRoad(roadTwo, IntersectPoint)[i - 2];
            }
            Intersection intersection = new Intersection(IntersectPoint, roadPartsPostSplit);
            WebNode webNode = new WebNode(intersection);

            (wns[0] = GetWebNode(roadOne.StartIntersection)).Item.ReplaceOption(roadOne, roadPartsPostSplit[0]);
            (wns[1] = GetWebNode(roadOne.EndIntersection)).Item.ReplaceOption(roadOne, roadPartsPostSplit[1]);
            (wns[2] = GetWebNode(roadTwo.StartIntersection)).Item.ReplaceOption(roadTwo, roadPartsPostSplit[2]);
            (wns[3] = GetWebNode(roadTwo.EndIntersection)).Item.ReplaceOption(roadTwo, roadPartsPostSplit[3]);

            webNode.NextPossibles.AddRange(new WebNode[]{wns[0], wns[2]});
            webNode.LastPossibles.AddRange(new WebNode[]{wns[1], wns[3]});

            bool temp = false;
            for(int i = 0; i < wns.Length; i++)
            {
                switch (temp)
                {
                    case true:
                        wns[i].LastPossibles.Remove(wns[i - 1]);
                        wns[i].LastPossibles.Add(webNode);
                        temp = !temp;
                        break;
                    case false:
                        wns[i].NextPossibles.Remove(wns[i + 1]);
                        wns[i].NextPossibles.Add(webNode);
                        temp = !temp;
                        break;
                }
            }
        }

        /*
         * Splits a road in two, and returns an array of the two new pieces
         */
        private Road[] SplitRoad(Road road, Point splitPoint)
        {
            Road[] temp = new Road[2];

            temp[0] = new Road(splitPoint, road.EndPoint, road.RoadWidth);
            temp[1] = new Road(road.StartPoint, splitPoint, road.RoadWidth);

            return temp;
        }
    }
}
