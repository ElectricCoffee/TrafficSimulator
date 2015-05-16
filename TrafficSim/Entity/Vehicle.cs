using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using TrafficSim.Event;
using TrafficSim.Util;


namespace TrafficSim.Entity
{
    public enum DrivingType { Drive, Brake }

    public abstract class Vehicle : IDrawable ,ISimulatable
    {
        /// <summary>
        /// THe Coordinate where the Vehicle are.
        /// </summary>
        public Point Coordinate {get; protected set;}

        /// <summary>
        /// The Vehicle max Acceleration.
        /// </summary>
        public int MaxAcc {get; set;}

        /// <summary>
        /// The Vehicle max Decceleration
        /// </summary>
        public int MaxDecc {get; set;}

        /// <summary>
        /// The Vehicles actual Acceleration
        /// </summary>
        public int Acc { get; set; }

        /// <summary>
        /// The Vehicle actual Deccelaeration
        /// </summary>
        public int Decc { get; set; }

        /// <summary>
        /// The Picture of the vehicle.
        /// </summary>
        public Image Picture;

        /// <summary>
        /// THe PIcturebox in where the picture is shown.
        /// </summary>
        public PictureBox PictureBox = new PictureBox();

        /// <summary>
        /// The Direction af the vehicle, set as a vector of the center of the Vehicle.
        /// </summary>
        public Point Direction { get; set; }

        /// <summary>
        /// A bbolean value to brake the vehicle.
        /// </summary>
        public bool IsBraking { get; set; }

        /// <summary>
        /// The actual speed of the vehicle.
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// The actual Driver of the vehicle
        /// </summary>
        public Driver Driver { get; set; }

        /// <summary>
        /// A boolean to accelerate the vehicle.
        /// </summary>
        public bool IsAcceleratin {get; set;}

        /// <summary>
        /// The graphic direktion of the vehicle.
        /// </summary>
        public RotateFlipType RotationType { get; set; }

        /// <summary>
        /// The road under the vehicle.
        /// </summary>
        public Road TheRoad { get; set; }

        public abstract int Length { get; protected set; }

        public TrafficEventHandler eventHandler { get; set; }

        public bool ASK { get; set; }

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


            if (IsBraking)
                ChangeGraphic(DrivingType.Brake); //Brakes();

            else ChangeGraphic(DrivingType.Drive); // UnBrakes();

            int cos = Direction.X;
            int sin = Direction.Y;

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

            ChangeRoad();
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

            if (ASK)
            {
                Car front = GetNearestCar();
                if (front != null)
                {
                    IsBraking = front.IsBraking;
                    IsAcceleratin = front.IsAcceleratin;
                    Speed = front.Speed;
                }
            }
           
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
        /// <param name="Angle">The Angle where the car is turning.</param>
        /// <param name="Time">the time the turn takes.</param>
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

        public void ChangeRoad()
        {
            if (TheRoad == null)
            {
                eventHandler.ClearEventsFromObject(this);
            }
            else if (TheRoad.StartPoint.X > TheRoad.EndPoint.X)
            {
                if (Coordinate.X < TheRoad.EndPoint.X)
                    TheRoad = TheRoad.Next;

            }
            else if (TheRoad.StartPoint.X < TheRoad.EndPoint.X)
            {
                if (Coordinate.X > TheRoad.EndPoint.X)
                    TheRoad = TheRoad.Next;
            }
            else if (TheRoad.StartPoint.Y > TheRoad.EndPoint.Y)
            {
                if (Coordinate.Y < TheRoad.EndPoint.Y)
                    TheRoad = TheRoad.Next;
            }
            else if (TheRoad.StartPoint.Y < TheRoad.EndPoint.Y)
            {
                if (Coordinate.Y > TheRoad.EndPoint.Y)
                    TheRoad = TheRoad.Next;
            }
            else
            {
                throw new StartAndEndPointIsEquelExeption();
            }
        }

        public Car GetNearestCar()
        {
            Car temp = null;
            double mindis = 1000;

            foreach(Car element in CarList.Cars)
            {
                if(element.Coordinate == this.Coordinate)
                {
                    // gør ingenting
                }
                else if(this.Direction.X >= 0 && this.Direction.Y >= 0 && this.Coordinate.X <= element.Coordinate.X && this.Coordinate.Y <= element.Coordinate.Y)
                {
                    if(GetLenght(element)<mindis)
                        temp = element;
                }
                else if(this.Direction.X <= 0 && this.Direction.Y >= 0 && this.Coordinate.X >= element.Coordinate.X && this.Coordinate.Y <= element.Coordinate.Y)
                {
                    if(GetLenght(element)<mindis)
                        temp = element;
                }
                else if(this.Direction.X >= 0 && this.Direction.Y <= 0 && this.Coordinate.X <= element.Coordinate.X && this.Coordinate.Y >= element.Coordinate.Y)
                {
                    if(GetLenght(element)<mindis)
                        temp = element;
                }
                else if(this.Direction.X <= 0 && this.Direction.Y <= 0 && this.Coordinate.X >= element.Coordinate.X && this.Coordinate.Y >= element.Coordinate.Y)
                {
                    if(GetLenght(element)<mindis)
                        temp = element;
                }
            }

            
            return temp;
        }

        public double GetLenght(Car obj)
        {
            return Math.Sqrt(Math.Pow((this.Coordinate.X - obj.Coordinate.X), 2) + Math.Pow((this.Coordinate.Y - obj.Coordinate.Y), 2));
        }
        
    }
}
