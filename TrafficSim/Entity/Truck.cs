using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrafficSim.Entity
{
    public class Truck : Vehicle
    {
        public Truck(int x, int y)
        {
            Coordinate = new Point(x, y);
            Picture = TrafficSim.Properties.Resources.TruckLeft;
            Length = 57;
        }

        public override int Length
        {
            get;
            protected set;
        }

        public override void ChangeGraphic(DrivingType dt)
        {
            switch (dt)
            {
                case DrivingType.Drive:
                    PictureBox.Image = TrafficSim.Properties.Resources.TruckLeft;
                    break;
                case DrivingType.Brake:
                    PictureBox.Image = TrafficSim.Properties.Resources.TruckLeftBrakes;
                    break;
                default:
                    throw new Exception("This shouldn't have happened");
            }
        }
    }
}
