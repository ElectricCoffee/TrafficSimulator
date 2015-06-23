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

        public static TrafficEventHandler Eventhandler = new TrafficEventHandler(new TimeSpan(0,0,1));

        public static Road VesterBro = new Road(new Point(50, 50), new Point(500, 50));
        
        public bool run{get;set;}

        public Random ran { get; set; }

        
        public Form1()
        {
            InitializeComponent();
            Form1.CheckForIllegalCrossThreadCalls = false;
            ran = new Random();
            Spawn();
            run = false;

        }
        /// <summary>
        /// Adder the vehicle image to the form, and calls the method Draw from the Vehicle.
        /// </summary>
        /// <param name="vehicle">The specefic vehicle witch will be drawed and added to the form.</param>
        void AddAndDrawVehicle(Vehicle vehicle)
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(()=>this.AddAndDrawVehicle(vehicle)));
            }

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
                
                Eventhandler.NextTick();
                Thread.Sleep(100);

            }
        }

        private void checkBoxASK_CheckedChanged(object sender, EventArgs e)
        {
            foreach(Driver element in DriverList.Drivers)
            {
                element.AssociatedVehicle.ASK = checkBoxASK.Checked;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                      
            
        }

        private void Spawn()
        {
            DriverList.Drivers.Add(new Driver()
            {
                AssociatedVehicle = new Car(50, 50)
                {
                    TheRoad = VesterBro,
                    MaxAcc = 1,
                    MaxDecc = 10,
                    EventHandler = Eventhandler,
                    Direction = new Point(100, 0),
                    Acc = 1,
                    Decc = 2,
                    Speed = 0,
                    IsBraking = true,
                    ASK = checkBoxASK.Checked,
                },
                EventHandler = Eventhandler,
                SpeedLimit = 3,
                ReactionTime = 1,
                SafeDistance = 55,
                VelocityTolerance = ran.Next(90, 150),

            });

            AddAndDrawVehicle(DriverList.Drivers[DriverList.Drivers.Count - 1].AssociatedVehicle);
            Eventhandler.AddContinuousEvent(DriverList.Drivers[DriverList.Drivers.Count - 1].Drive);

            Eventhandler.AddDiscreteEvent(Spawn, new TimeSpan(0, 0, 60/ trackBarTrafficFlow.Value));
        }

        private void trackBarTrafficFlow_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = trackBarTrafficFlow.Value + " biler i minutet.";
        }
        
    }
}
