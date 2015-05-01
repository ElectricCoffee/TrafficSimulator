using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim
{
    class TraficLight : ISign
    {

        private int RedTime;
        private int YellowTime = 5;
        private int GreenTime;

        public void SetRedTime(int RedTime)
        {
            this.RedTime = RedTime;
        }

        public void SetGreenTime(int GreenTime)
        {
            this.GreenTime = GreenTime;
        }
    }
}
