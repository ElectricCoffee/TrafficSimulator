using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Entity
{
    public class Driver
    {
        public int VelocityTolerance { get; set; }
        public int ReactionTime { get; set; }
        public DriverAggression Aggression { get; set; }
        public int SafeDistance { get; set; }
        public int SpeedLimit { get; set; }

        private void SetTargets()
        {

        }

        private void ReadSign()
        {

        }
    }

    public struct DriverAggression
    {
        public DriverAggression(int acc, int dec)
        {
            Acceleration = acc;
            Deceleration = dec;
        }

        public int Acceleration { get; private set; }
        public int Deceleration { get; private set; }
    }
}
