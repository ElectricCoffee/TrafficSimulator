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

        public RestrictionType UseRestriction { get; set; }

        public int SpeedLimit { get; set; }

    }
}
