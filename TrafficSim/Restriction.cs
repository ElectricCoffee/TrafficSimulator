using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    class Restriction : ISign
    {

        /// <summary>
        /// Get og set hastighedsbegrænsning
        /// </summary>

        public int SpeedLimit { get; set; }

    }
}
