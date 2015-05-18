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

            TraficLight light = new TraficLight(50, 50);
            light.Animate(5, Timeinput);
            Controls.Add(light.TLight);
            

        }
    }
}
