using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TrafficSim;
using TrafficSim.Entity.Proxy;

namespace TrafficSim.Entity
{
    public class RoadWeb
    {
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
            {
                new Thread(() => IntersectPoint(road)).Start();
            }
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
        /// Checks if the inputted road intersects with any roads already part of the roadweb
        /// Assumes all the roads can be put into either y = ax+b or y = b
        /// Comments coming soon™
        /// </summary>
        /// <param name="FirstRoad">The Road that possibly might intersect</param>
        /// <returns></returns>
#warning IntersectPoint & InsertIntersection are not yet completed
        private bool IntersectPoint(Road FirstRoad)
        {
            foreach (WebNode node in completeRoadList)
                foreach (Road SecondRoad in node.Item.IntersectionExits)
                {
                    //this is to ensure a float division instead of an integer division
                    float FirstStartItem1Calc = FirstRoad.StartPoint.Item1;
                    float FirstEndItem1Calc = FirstRoad.EndPoint.Item1;
                    float FirstStartItem2Calc = FirstRoad.StartPoint.Item2;
                    float FirstEndItem2Calc = FirstRoad.EndPoint.Item2;

                    //it's the a in y = ax + b
                    float FirstSlope = (FirstStartItem2Calc - FirstEndItem2Calc) /
                                     (FirstStartItem1Calc - FirstEndItem1Calc);

                    float SecondStartItem1Calc = SecondRoad.StartPoint.Item1;
                    float SecondEndItem1Calc = SecondRoad.EndPoint.Item1;
                    float SecondStartItem2Calc = SecondRoad.StartPoint.Item2;
                    float SecondEndItem2Calc = SecondRoad.EndPoint.Item2;

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
                            return false;

                        else return true;
                    }
                    else if (SecondStartItem1Calc - SecondStartItem1Calc == 0)
                    {
                        if (FirstStart >= (SecondStartItem2Calc > SecondEndItem2Calc ? SecondStartItem2Calc : SecondEndItem2Calc) || (
                            FirstStart <= (SecondStartItem2Calc < SecondEndItem2Calc ? SecondStartItem2Calc : SecondEndItem2Calc))
                          )
                            return false;
                        else return true;
                    }
                    //assumes that if the roads have the same growth
                    if (FirstSlope != SecondSlope)
                    {
                        //the points that they cross
                        //assumes that at some point they will cross
                        //only takes into consideration the lines equation y = ax+b
                        float CrossingPointY = (SecondSlope * -FirstStart + FirstSlope * SecondStart) / (FirstSlope - SecondSlope);
                        float CrossingPointX = (CrossingPointY - SecondStart) / SecondSlope;

                        //checks whether it crosses at a point within the line or not
                        if ((
                                    CrossingPointX >= (FirstRoad.StartPoint.Item1 > FirstRoad.EndPoint.Item1 ?
                                                       FirstRoad.StartPoint.Item1 : FirstRoad.EndPoint.Item1) || (
                                    CrossingPointX >= (SecondRoad.StartPoint.Item1 > SecondRoad.EndPoint.Item1 ?
                                                       SecondRoad.StartPoint.Item1 : SecondRoad.EndPoint.Item1)) || (
                                    CrossingPointX <= (FirstRoad.StartPoint.Item1 < FirstRoad.EndPoint.Item1 ?
                                                       FirstRoad.StartPoint.Item1 : FirstRoad.EndPoint.Item1)) ||
                                    CrossingPointX <= (SecondRoad.StartPoint.Item1 < SecondRoad.EndPoint.Item1 ?
                                                       SecondRoad.StartPoint.Item1 : SecondRoad.EndPoint.Item1)) && (
                                    CrossingPointY >= (FirstRoad.StartPoint.Item2 > FirstRoad.EndPoint.Item2 ?
                                                       FirstRoad.StartPoint.Item2 : FirstRoad.EndPoint.Item2)) || (
                                    CrossingPointY >= (SecondRoad.StartPoint.Item2 > SecondRoad.EndPoint.Item2 ?
                                                       SecondRoad.StartPoint.Item2 : SecondRoad.EndPoint.Item2)) || (
                                    CrossingPointY <= (FirstRoad.StartPoint.Item2 < FirstRoad.EndPoint.Item2 ?
                                                       FirstRoad.StartPoint.Item2 : FirstRoad.EndPoint.Item2)) ||
                                    CrossingPointY <= (SecondRoad.StartPoint.Item2 < SecondRoad.EndPoint.Item2 ?
                                                       SecondRoad.StartPoint.Item2 : SecondRoad.EndPoint.Item2)
                        )
                        {
                            return false;
                        }
                        else 
                        {
                            //This one's for when they cross, the other's for when they don't
                            InsertIntersection(FirstRoad, SecondRoad, new Tuple<int, int>((int)CrossingPointX, (int)CrossingPointY));
                            return true;
                        }
                    } 
                    return false;
                }
            return false;
        }

        private void InsertIntersection(Road roadOne, Road roadTwo, Tuple<int, int> IntersectPoint)
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
        private Road[] SplitRoad(Road road, Tuple<int, int> splitPoint)
        {
            Road[] temp = new Road[2];

            temp[0] = new Road(splitPoint, road.EndPoint, road.RoadWidth);
            temp[1] = new Road(road.StartPoint, splitPoint, road.RoadWidth);

            return temp;
        }
    }
}
