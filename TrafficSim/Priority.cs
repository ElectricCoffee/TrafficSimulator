using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{

    public enum PriorityType
    {
        Stop, GiveWay, OncompingPriority
    }

    class Priority : Sign
    {

        public PriorityType UsePriority { get; set; }

    }
}
