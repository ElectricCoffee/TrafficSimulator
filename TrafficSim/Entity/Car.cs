using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TrafficSim.Entity
{
    class Car : Vehicle
    {

        /// <summary>
        /// Make an instanc of Car and sets it coordinates.
        /// </summary>
        /// <param name="x">The x for the car.</param>
        /// <param name="y">THe y for the car.</param>
        public Car(int x, int y)
        {
            Coordinat = new Point(x, y);
            Picture = TrafficSim.Properties.Resources.CarLeft;
        }

        public override void Brakes()
        {
            PictureBox.Image = TrafficSim.Properties.Resources.CarLeftBrakes;
        }

        public override void UnBrakes()
        {
            PictureBox.Image = TrafficSim.Properties.Resources.CarLeft;
        }
        
    }
}
