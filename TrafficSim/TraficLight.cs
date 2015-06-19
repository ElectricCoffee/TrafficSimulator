using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TrafficSim
{
    class TraficLight : ISign
    {

        public Point Location { get; set; }
        public bool IsGreen { get; set; }
        public PictureBox TLight = new PictureBox();

        private void DrawRedLight()
        {
            TLight.Image = TrafficSim.Properties.Resources.Red;
        }

        private void DrawYellowLight()
        {
            TLight.Image = TrafficSim.Properties.Resources.Yellow;
        }

        private void DrawGreenLight()
        {
            TLight.Enabled = true;
            TLight.Image = TrafficSim.Properties.Resources.Green;

        }

        public TraficLight(int PosX, int PosY)
        {
            /// <summary>
            /// Sætter lysmastens position og starter den som rød
            /// </summary>
            Location = new Point(PosX, PosY);
            DrawRedLight();
            IsGreen = false;
        }

        public void Animate(int Cycelstart, TimeSpan Time)
        {
            /// <summary>
            /// Skifer lysets farve når det er tid til det
            /// </summary>
            if (Time.Seconds % Cycelstart == null || Time.Seconds % (Cycelstart + Cycelstart) == null)
            {
                DrawYellowLight();
            }

            if (Time.Seconds % (Cycelstart + 1) == null)
            {
                DrawGreenLight();
                IsGreen = true;
            }
            else if (Time.Seconds % (Cycelstart + Cycelstart + 1) == null)
            {
                DrawRedLight();
                IsGreen = false;
            }
        }
    }
}
