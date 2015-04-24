using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using TrafficSim.Event;

namespace TrafficSim.Entity
{
    public class Car : Vehicle
    {

        /// <summary>
        /// Make an instanc of Car and sets it coordinates.
        /// </summary>
        /// <param name="x">The x for the car.</param>
        /// <param name="y">THe y for the car.</param>
        public Car(int x, int y, TrafficEventHandler eventHandler)
        {
            Coordinate = new Point(x, y);
            Picture = TrafficSim.Properties.Resources.CarLeft;
            base.EventHandler = eventHandler;
        }

        public override void ChangeGraphic(DrivingType dt)
        {
            switch (dt)
            {
                case DrivingType.Drive:
                    PictureBox.Image = TrafficSim.Properties.Resources.CarLeft;
                    break;
                case DrivingType.Brake:
                    PictureBox.Image = TrafficSim.Properties.Resources.CarLeftBrakes;
                    break;
                default:
                    break;
            }
        }        
    }
}
