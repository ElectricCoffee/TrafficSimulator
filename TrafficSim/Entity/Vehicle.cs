using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace TrafficSim.Entity
{
    abstract class Vehicle : IDrawable
    {
        public Point Coordinat {get; protected set;}
        public int MaxAcc {get; protected set;}
        public int MaxDecc {get; protected set;}
        public int Acc { get; set; }
        public int Decc { get; set; }
        public Image Picture;
        public PictureBox PictureBox = new PictureBox();
        public Point Direction { get; set; }
        public bool BrakeBool { get; set; }
        public int Speed { get; set; }

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
        
        /// <summary>
        /// Sets the new speed, by acceleration and moves the vehicle, by the time.
        /// </summary>
        /// <param name="milisecond">The time in miliseconds that will set the speed, and the new location by diredtion.</param>
        public void Accelerate(int milisecond)
        {
            double direntionLenght = Math.Sqrt(Math.Pow(Coordinat.X - Direction.X, 2) + Math.Pow(Coordinat.Y - Direction.Y, 2));
            double directionEnhedX = Direction.X / direntionLenght;
            double directionEnhedY = Direction.Y / direntionLenght;

            if (BrakeBool)
                Speed -= Acc * milisecond/1000;
            else
                Speed += Acc * milisecond/1000;
            int lenght = Speed * milisecond/1000 * 8; //8px pr. m
            Move(Coordinat.X + (int)(directionEnhedX * lenght), Coordinat.Y + (int)(directionEnhedY * lenght));
        }
        
    }
}
