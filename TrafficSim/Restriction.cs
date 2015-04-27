using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{

    public enum RestrictionType
    {
        NoEntry, NoOvertaking
    }

    class Restriction : Sign
    {

        /// <summary>
        /// Get og Set hvlke restriktioner der skal bruges
        /// </summary>

        public RestrictionType UseRestriction { get; set; }

        /// <summary>
        /// Get og Set hvad fartgrænsen skal være
        /// </summary>

        public int SpeedLimit { get; set; }

    }
}
