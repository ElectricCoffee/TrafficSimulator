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


namespace TrafficSim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            Car bil = new Car(100, 100);
            
            AddAndDrawVehicle(bil);
            bil.Direction = new Point(0,-30);
            bil.Acc = 2;
            bil.BrakeBool = false;
            bil.Accelerate(3000);

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
