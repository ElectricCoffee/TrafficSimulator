using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TrafficSim
{
    class TraficLight : ISign
    {

        public Point Location { get; set; }
        public PictureBox TLight = new PictureBox();
        
        public void DrawRedLight()
        {

            TLight.Image = TrafficSim.Properties.Resources.Red;

        }

        public void DrawYellowLight()
        {

            TLight.Image = TrafficSim.Properties.Resources.Yellow;

        }

        public void DrawGreenLight()
        {
            TLight.Enabled = true;
            TLight.Image = TrafficSim.Properties.Resources.Green;

        }

    }
}
