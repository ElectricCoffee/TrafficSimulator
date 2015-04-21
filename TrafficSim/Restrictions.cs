using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    public enum RestrictionType
    {
        OneWay, NoEntry, GiveWay, NoOvertaking
    }


    public class Restrictions : Sign
    {
        public RestrictionType Restriction { get; set; }

        public Restrictions(RestrictionType rt)
        {
            Restriction = rt;
        }

    }
}
