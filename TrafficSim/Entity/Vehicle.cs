using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;



namespace TrafficSim.Entity
{
    public enum DrivingType { Drive, Brake }

    public abstract class Vehicle : IDrawable 
    {
        public Point Coordinate {get; protected set;}
        public int MaxAcc {get; protected set;}
        public int MaxDecc {get; protected set;}
        public int Acc { get; set; }
        public int Decc { get; set; }
        public Image Picture;
        public PictureBox PictureBox = new PictureBox();
        public Point Direction { get; set; }
        public bool IsBreaking { get; set; }
        public int Speed { get; set; }
        public Driver Driver { get; set; }
        public bool IsAcceleratin {get; set;}

        /// <summary>
        /// Drawing the car, at it's coordinates.
        /// </summary>
        public void Draw()
        {
            PictureBox.Location = Coordinate;
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
            Coordinate = new Point(x, y);
            PictureBox.Location = Coordinate;

            int cos = Direction.X;
            int sin = Direction.Y;

            if (IsBreaking)
                ChangeGraphic(DrivingType.Brake); //Brakes();

            else ChangeGraphic(DrivingType.Drive); // UnBrakes();

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
        /// Changes the graphic of the vehicle, depending on if it's driving or not
        /// </summary>
        /// <param name="dt">The type of driving, braking/driving</param>
        public abstract void ChangeGraphic(DrivingType dt);
        
        /// <summary>
        /// Sets the new speed, by acceleration and moves the vehicle, by the time.
        /// </summary>
        /// <param name="milisecond">The time in miliseconds that will set the speed, and the new location by diredtion.</param>
        public void Drive(int milisecond)
        {
            if (Direction.X == 0 && Direction.Y == 0)
                throw new Util.NoDirectionException();      //trow exception

            double direntionLenght = Math.Sqrt(Math.Pow(Direction.X, 2) + Math.Pow(Direction.Y, 2));
            double directionEnhedX = Direction.X / direntionLenght;
            double directionEnhedY = Direction.Y / direntionLenght;

            if (IsBreaking)
                Speed -= Decc * milisecond/1000;
            else if (IsAcceleratin)
                Speed += Acc * milisecond/1000;

            int lenght = Speed * milisecond/1000 * 8; //8px pr. m
            Move(Coordinate.X + (int)(directionEnhedX * lenght), Coordinate.Y + (int)(directionEnhedY * lenght));
        }
        
    }
}
