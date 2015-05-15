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
using TrafficSim.Event;
using System.Threading;

namespace TrafficSim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            TrafficEventHandler Eventhandler = new TrafficEventHandler(new TimeSpan(100));

            Road VesterBro = new Road(new Point(50, 50), new Point(500, 50));
            Car bil1 = new Car(50, 50)
            {
                TheRoad = VesterBro,
                MaxAcc = 1,
                MaxDecc = 10,
                Driver = new Driver(),
                eventHandler = Eventhandler,
            };

            AddAndDrawVehicle(bil1);

            while(true)
            {
                Eventhandler.NextTick();
                Thread.Sleep(100);

            }
           
            
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
