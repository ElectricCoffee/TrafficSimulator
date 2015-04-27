using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using TrafficSim.Entity;
using System.Diagnostics;

namespace TrafficSim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            Car car1 = new Car(50, 50);
            
            AddAndDrawVehicle(car1);
            
            car1.Direction = new Point(50, 0);
            car1.Speed = 50;
            car1.TurnCar(1.5, new TimeSpan(0, 0, 2));

            Truck lastbil = new Truck(50, 50);
            AddAndDrawVehicle(lastbil);
            
            
        }
        /// <summary>
        /// Adder the vehicle image to the form, and calls the method Draw from the Vehicle.
        /// </summary>
        /// <param name="vehicle">The specefic vehicle witch will be drawed and added to the form.</param>
        void AddAndDrawVehicle(Vehicle vehicle)
        {
            Controls.Add(vehicle.PictureBox);
            vehicle.Draw();
        }
        

        

        
    }
}
