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
        public int x {get; protected set;}
        public int y {get; protected set;}
        public int MaxAcc {get; protected set;}
        public int MaxDecc {get; protected set;}
        public PictureBox Image = new PictureBox();


        public void Draw()
        {
            Image.Location = new Point(x, y);
            Image.Visible = true;
            Image.Enabled = true;
            //Image.Size = new Size(50, 50);
            Image.Image = TrafficSim.Properties.Resources.CarLeft;
        }

        public void Move(int x, int y)
        {
            this.x = x;
            this.y = y;
            Image.Location = new Point(x, y);
            Image.Update();
        }
    }
}
