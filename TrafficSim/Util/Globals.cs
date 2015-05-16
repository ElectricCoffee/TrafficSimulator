using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Util
{
    /// <summary>
    /// Static class that holds global values and methods.
    /// DO NOT USE THIS FOR VARIABLE DATA, IT CAN LEAD TO UNPREDICTABLE BEHAVIOUR
    /// </summary>
    public static class Globals
    {
        public static readonly int AggressionMax = 100;
        public static readonly int AggressionMin = 10;

        public static readonly int VehicleStartX = 50;
        public static readonly int VehicleStartY = 50;
    }
}
