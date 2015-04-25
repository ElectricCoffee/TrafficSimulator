using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Event
{
    /// <summary>
    /// Every object used by TrafficEventHandler should inherit ISimulatable.
    /// </summary>
    public interface ISimulatable
    {
        TrafficEventHandler eventHandler { get; set; }
    }
}