using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrafficSim.Entity
{
    class Truck : Vehicle
    {
        public Truck(int x, int y)
        {
            Coordinat = new Point(x, y);
            Picture = TrafficSim.Properties.Resources.TruckLeft;
        }

        public override void Brakes()
        {
            PictureBox.Image = TrafficSim.Properties.Resources.TruckLeftBrakes;
        }

        public override void UnBrakes()
        {
            PictureBox.Image = TrafficSim.Properties.Resources.TruckLeft;
        }
        
    }
}
