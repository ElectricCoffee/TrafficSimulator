using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;


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
        public bool IsBraking { get; set; }
        public int Speed { get; set; }
        public Driver Driver { get; set; }
        public bool IsAcceleratin {get; set;}
        public RotateFlipType RotationType { get; set; }
        public Road TheRoad { get; set; }

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

            if (IsBraking)
                ChangeGraphic(DrivingType.Brake); //Brakes();

            else ChangeGraphic(DrivingType.Drive); // UnBrakes();

            if (cos < 0 && sin < 0)
                RotationType = RotateFlipType.RotateNoneFlipNone;
            else if (cos > 0 && sin < 0)
                RotationType = RotateFlipType.Rotate90FlipNone;
            else if (cos > 0 && sin > 0)
                RotationType = RotateFlipType.Rotate180FlipNone;
            else
                RotationType = RotateFlipType.Rotate270FlipNone;

            PictureBox.Image.RotateFlip(RotationType);

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
        public void Drive(TimeSpan Time)
        {
            if (Direction.X == 0 && Direction.Y == 0)
                throw new Util.NoDirectionException();      //trow exception

            double direntionLenght = Math.Sqrt(Math.Pow(Direction.X, 2) + Math.Pow(Direction.Y, 2));
            double directionEnhedX = Direction.X / direntionLenght;
            double directionEnhedY = Direction.Y / direntionLenght;

            if (IsBraking)
                Speed -= Decc * Time.Seconds;
            else if (IsAcceleratin)
                Speed += Acc * Time.Seconds;

            int lenght = Speed * Time.Seconds * 8; //8px pr. m
            Move(Coordinate.X + (int)(directionEnhedX * lenght), Coordinate.Y + (int)(directionEnhedY * lenght));
        }

        /// <summary>
        /// Turn and move the car
        /// </summary>
        /// <param name="Angle">The Angel</param>
        /// <param name="Time"></param>
        public void TurnCar(double Angle, TimeSpan Time)
        {
            double newX = RotateX(Direction.X, Direction.Y, Angle) + Coordinate.X - RotateX(Coordinate.X, Coordinate.Y, Angle);
            double newY = RotateY(Direction.X, Direction.Y, Angle) + Coordinate.Y - RotateY(Coordinate.X, Coordinate.Y, Angle);
            Direction = new Point((int)newX, (int)newY);
            Drive(Time);
        }

        private double RotateX(int X, int Y, double Angle)
        {
            return Math.Cos(Angle) * X - Math.Sin(Angle) * Y;
        }

        private double RotateY(int X, int Y, Double Angle)
        {
            return Math.Sin(Angle) * X + Math.Cos(Angle) * Y;
        }
        
    }
}
