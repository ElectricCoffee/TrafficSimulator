using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrafficSim.Entity
{
    public class Driver
    {
        public int VelocityTolerance { get; set; }
        public int ReactionTime { get; set; }
        public DriverAggression Aggression { get; set; }
        public int SafeDistance { get; set; }
        public int SpeedLimit { get; set; }
        public Point NearPoint { get; set; }
        public Point FarPoint { get; set; }


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
    /// </summary>
    public struct DriverAggression
    {
        public DriverAggression(double acc, double dec)
        {
            Acceleration = acc;
            Deceleration = dec;
        }

        /// <summary>
        /// The driver's willingness to accelerate in %
        /// </summary>
        public double Acceleration { get; private set; }
        /// <summary>
        /// The driver's willingness to decelerate in %
        /// </summary>
        public double Deceleration { get; private set; }
    }
}
