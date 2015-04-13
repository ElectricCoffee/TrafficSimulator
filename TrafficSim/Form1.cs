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


namespace TrafficSim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            Car bil = new Car(100, 100);
            
            AddAndDrawVehicle(bil);
            bil.Direction = new Point(0,80);
            bil.BrakeBool = true;
            bil.Move(bil.Coordinat.X + 50, bil.Coordinat.Y);

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
