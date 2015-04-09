using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TrafficSim
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            Car bil = new Car(100, 100);
            AddAndDrawCar(bil);
            
            bil.Move(bil.x + 50, bil.y);
            
        }
        
        void AddAndDrawCar(Car car)
        {
            Controls.Add(car.Image);
            car.Draw();
        }
        

        

        
    }
}
