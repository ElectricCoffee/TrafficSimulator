using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
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
