using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Event
{
    public interface ISimulatable
    {
        TrafficEventHandler eventHandler { get; set; }
    }
}