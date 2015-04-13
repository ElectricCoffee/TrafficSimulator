using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace TrafficSim
{
    abstract class Vehicle : IDrawable
    {
        public int hej { get; set; }

        public Point Coordinat {get; protected set;}
        public int MaxAcc {get; protected set;}
        public int MaxDecc {get; protected set;}
        public Image Picture;
        public PictureBox PictureBox = new PictureBox();
        public Point Near { get; set; }
        public Point Far { get; set; }
        public Point Direction { get; set; }
        public bool BrakeBool { get; set; }

        /// <summary>
        /// Drawing the car, at it's coordinates.
        /// </summary>
        public void Draw()
        {
            PictureBox.Location = Coordinat;
            PictureBox.Visible = true;
            PictureBox.Enabled = true;
            
            PictureBox.Image = Picture;
        }


        /// <summary>
        /// Moving the car and point it at it's far point.
        /// </summary>
        /// <param name="x">The new x for the car.</param>
        /// <param name="y">The new y for the car.</param>
        public void Move(int x, int y)
        {
            this.Coordinat = new Point(x, y);
            PictureBox.Location = Coordinat;
            int cos = Coordinat.X - Direction.X;
            int sin = Coordinat.Y - Direction.Y;

            if (BrakeBool)
                this.Brakes();
            else this.UnBrakes();

            if (cos < 0 && sin < 0)
                Picture.RotateFlip(RotateFlipType.RotateNoneFlipNone);
            else if (cos > 0 && sin < 0)
                Picture.RotateFlip(RotateFlipType.Rotate90FlipNone);
            else if (cos > 0 && sin > 0)
                Picture.RotateFlip(RotateFlipType.Rotate180FlipNone);
            else
                Picture.RotateFlip(RotateFlipType.Rotate270FlipNone);

            PictureBox.Update();
            
        }

        /// <summary>
        /// Change the Vehicle image to brakes.
        /// </summary>
        public abstract void Brakes();
        /// <summary>
        /// Change the Vehicle image to normal.
        /// </summary>
        public abstract void UnBrakes();


        
    }
}
