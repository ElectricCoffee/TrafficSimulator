using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TrafficSim
{
    abstract class Vehicle : IDrawable
    {
        public int x;
        public int y;
        public int MaxAcc;
        public int MaxDecc;
        public PictureBox Image = new PictureBox();


        public void Draw()
        {
            Image.Location = new Point(x, y);
            Image.Visible = true;
            Image.Enabled = true;
            Image.Size = new Size(50, 50);
            Image.Image = TrafficSim.Properties.Resources.CarLeft;

        }
    }
}
