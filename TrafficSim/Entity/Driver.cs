using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using TrafficSim.Util;

namespace TrafficSim.Entity
{
    public class Driver
    {
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

#warning TODO: implement distance algorithm
        public int SafeDistance 
        { 
            get 
            {
                throw new Exception("SafeDistance not currently Implemented");
            } 
        }

        /// <summary>
        /// Basic constructor, creates a new driver, and a new driver aggression
        /// </summary>
        public Driver()
        {
            Aggression = new DriverAggression();
        }

        public static Driver CreateRandom()
        {
            var driver = new Driver();
            var rndm = new Random(); // the empty constructor uses time as a seed by default.
            driver.Aggression.Acceleration = rndm.Next(Globals.AggressionMin, Globals.AggressionMax);
            driver.Aggression.Deceleration = rndm.Next(Globals.AggressionMin, Globals.AggressionMax);
            // randomiser code goes here
            return driver;
        }


        /// <summary>
        /// Sets the near and far points
        /// </summary>
        protected void SetTargets()
        {
            
        }

        /// <summary>
        /// Reads an incoming sign and changes the speed-limit accordingly
        /// </summary>
        protected void ReadSign()
        {

        }
    }

    /// <summary>
    /// Contains the different aggression types the driver can have
    /// The aggression types define how quickly the driver accelerates and decelerates
    /// </summary>
    public class DriverAggression
    {
        public DriverAggression() : this(0, 0) { }

        public DriverAggression(int acc, int dec)
        {
            Acceleration = acc;
            Deceleration = dec;
        }

        /// <summary>
        /// The driver's willingness to accelerate in %
        /// </summary>
        public int Acceleration { get; set; }
        /// <summary>
        /// The driver's willingness to decelerate in %
        /// </summary>
        public int Deceleration { get; set; }
    }
}
