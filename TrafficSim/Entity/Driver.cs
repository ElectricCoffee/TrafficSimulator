using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using TrafficSim.Util;
using TrafficSim.Util.Extensions;

namespace TrafficSim.Entity
{
    /// <summary>
    /// The state as described in the "A Cognitive Model of Drivers Attention"
    /// </summary>
    public enum DriverState
    {
        DontOvertake, 
        Overtake, 
        NoIntersection, 
        FoundIntersection, 
        IntersectionNotInSafeDistance, 
        IntersectionInSafeDistance, 
        NoTraffic, 
        VehicleOnRightSide,
        VehicleGone, 
        VehicleStillThere
    }

    public class Driver : Event.ISimulatable
    {
        public Event.TrafficEventHandler EventHandler { get; set; }

        /// <summary>
        /// The vehicle associated with the driver
        /// </summary>
        public Vehicle AssociatedVehicle { get; protected set; }

        /// <summary>
        /// How well the driver respect the traffic law
        /// </summary>
        public int VelocityTolerance { get; protected set; }

        /// <summary>
        /// How quickly/slowly the driver can react
        /// </summary>
        public int ReactionTime { get; protected set; }

        /// <summary>
        /// How suddenly the driver accelerates or decelerates
        /// </summary>
        public DriverAggression Aggression { get; protected set; }

        /// <summary>
        /// The current speed limit
        /// </summary>
        public int SpeedLimit { get; protected set; }

        /// <summary>
        /// Determines the position on the road
        /// </summary>
        public Point NearPoint { get; protected set; }

        /// <summary>
        /// Determines direction of driving
        /// Is set at the 'vanishing point'or leading vehicle
        /// </summary>
        public Point FarPoint { get; protected set; }

        /// <summary>
        /// Determines the safe distance
        /// </summary>
        public int SafeDistance { get { return AssociatedVehicle.Length; } }

        /// <summary>
        /// The driver's name, used for easier tracking
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Basic constructor, creates a new driver, and a new driver aggression
        /// </summary>
        public Driver()
        {
            Aggression = new DriverAggression();
        }

        /// <summary>
        /// Creates a new Driver with an associated TrafficEventHandler
        /// </summary>
        /// <param name="eventHandler">The event handler, that will be used to handle delegated events</param>
        public Driver(Event.TrafficEventHandler eventHandler) : this()
        {
            EventHandler = eventHandler;
        }

        /// <summary>
        /// Creates a new driver with randomised attributes.
        /// </summary>
        /// <returns>new driver with randomised attributes</returns>
        public static Driver CreateRandom(Event.TrafficEventHandler eventHandler)
        {
            var driver = new Driver(eventHandler);
            var rndm = new Random(); // the empty constructor uses time as a seed by default.
            Func<int> randomAggroLevel = () => rndm.Next(Globals.AggressionMin, Globals.AggressionMax);

            driver.Aggression.Acceleration = randomAggroLevel();
            driver.Aggression.Deceleration = randomAggroLevel();

            // assigns a new vehicle at random, making sure the ratio of trucks is lower than cars
            driver.AssociatedVehicle = rndm.Next(int.MaxValue) % 7 == 0 
                ? new Truck(Globals.VehicleStartX, Globals.VehicleStartY) as Vehicle
                : new Car(Globals.VehicleStartX, Globals.VehicleStartY) as Vehicle;

            // randomiser code goes here
            return driver;
        }


        /// <summary>
        /// Sets the near and far points
        /// </summary>
        protected void SetPoints()
        {
            // the direction dictates where the vehicle goes, 
            // ( 0, 1) is up, (1, 0) is right 
            // (-1, 0) is left, and (0, -1) is down
            var direction = AssociatedVehicle.Direction;
            var coord = AssociatedVehicle.Coordinate;

            // set the near point just in front of the vehicle
#warning 4 is just an arbitrary number, consult group for actual value
            NearPoint = new Point(coord.X + direction.X * 4, coord.Y + direction.Y * 4);

            // set the far point "at the horizon" this distance needs to be discussed
            // if there's a leading vehicle closer than the horizon, 
            // we need to set the far point at the leading vehicle
        }

        /// <summary>
        /// Reads an incoming sign and changes the speed-limit accordingly
        /// </summary>
        protected void ReadSign()
        {
            // TODO: Scan the current road section for signs, then apply the signs (requires finished Road class)
            var signs = AssociatedVehicle.TheRoad.Signs;

            try
            {
                foreach (var sign in signs)
                {
#warning Can't continue without a finished Sign class
                    // do stuff with the sign
                };
            }
            catch (NullReferenceException nre)
            {
                Util.Error.Log(nre);

                throw new NullReferenceException("There's no allocated sign list", nre);
            }
        }

        /// <summary>
        /// Reacts to the driver ahead based on safe-distance and near+far points
        /// </summary>
        protected void React()
        {
            if(AssociatedVehicle.GetLenght(AssociatedVehicle.GetNearestCar()) < NearPoint.LenghtBetween(AssociatedVehicle.Coordinate))
            {
                AssociatedVehicle.IsBraking = true;
                AssociatedVehicle.IsAcceleratin = false;

                EventHandler.AddContinuousEvent(() => AssociatedVehicle.Drive(EventHandler.TickLength));
            }
        }

    }
}
