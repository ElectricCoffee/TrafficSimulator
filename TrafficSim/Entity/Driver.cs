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
        public Vehicle AssociatedVehicle { get; set; }
        public int VelocityTolerance { get; set; }
        public int ReactionTime { get; set; }
        public DriverAggression Aggression { get; set; }
        public int SpeedLimit { get; set; }
        public Point NearPoint { get; set; }
        public Point FarPoint { get; set; }

#warning TODO: implement distance algorithm
        public int SafeDistance 
        { 
            get 
            {
                throw new Exception("SafeDistance not currently Implemented");
            } 
        }

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

        // conflict showcase

        /// <summary>
        /// Sets the near and far points
        /// </summary>
        private void SetTargets()
        {
            
        }

        /// <summary>
        /// Reads an incoming sign and changes the speed-limit accordingly
        /// </summary>
        private void ReadSign()
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
