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

        public static TrafficEventHandler Eventhandler = new TrafficEventHandler(new TimeSpan(100));

        public static Road VesterBro = new Road(new Point(50, 50), new Point(500, 50));
        
        

        public bool run{get;set;}
        public Form1()
        {
            InitializeComponent();
            Form1.CheckForIllegalCrossThreadCalls = false;
            CarList.Cars.Add(new Car(50, 50)
        {
            TheRoad = VesterBro,
            MaxAcc = 1,
            MaxDecc = 10,
            Driver = new Driver(),
            EventHandler = Eventhandler,
            Direction = new Point(100, 0),
        });

            AddAndDrawVehicle(CarList.Cars[0]);

            run = false;
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

        private void buttonRun_Click(object sender, EventArgs e)
        {


            if(buttonRun.Text == "Run")
            {
                buttonRun.Text = "Stop";
                run = true;
            }
            else
            {
                buttonRun.Text = "Run";
                run = false;
            }


            Thread Simulation = new Thread(new ThreadStart(start));
            Simulation.Start();
            
        }
        public void start()
        {
            while (run)
            {
                CarList.Cars[0].Drive(new TimeSpan(100));
                Eventhandler.NextTick();
                Thread.Sleep(100);

            }
        }
        
    }
}
