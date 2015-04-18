using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficSim.Entity;
using System.Drawing;
using System.Windows.Forms;


namespace UnitTests
{
    [TestClass]
    public class VehicleTest
    {
        [TestMethod]
        public void DrawTestParameterTest()
        {
            DrawTest(50, 50, 50, 50);
            DrawTest(100, 60, 100, 60);
            DrawTest(0, 0, 0, 0);
        }


        public void DrawTest(int x,int y, int ex, int ey)
        {
            Car car1 = new Car(x, y);
            car1.Draw();
            Assert.AreEqual(car1.PictureBox.Location, new Point(ex, ey));
        }

        [TestMethod]
        public void MoveTest()
        {
            Car car1 = new Car(50, 50);
            car1.Move(100, 50);
            Assert.AreEqual(car1.Coordinate, new Point(100, 50));
            Assert.AreEqual(car1.PictureBox.Location, new Point(100, 50));
        }

        [TestMethod]
        public void RotationTest()
        {
            Car car1 = new Car(50, 50);
            car1.Direction = new Point(-50, 100);
            car1.Move(40, 60);
            Assert.AreEqual(car1.RotationType, RotateFlipType.Rotate270FlipNone);
        }

        [TestMethod]
        public void AccelerationTest() 
        {
            Car car1 = new Car(50, 50);
            car1.Direction = new Point(50, 0);
            car1.Acc = 1;
            car1.IsAcceleratin = true;
            car1.Drive(2000);
            Assert.AreEqual(car1.Coordinate, new Point(82,50));
            Assert.AreEqual(car1.PictureBox.Location, new Point(82, 50));
        }

        [TestMethod]
        public void DecelerationTest()
        {
            Car car1 = new Car(50, 50);
            car1.Direction = new Point(50, 0);
            car1.Decc = 1;
            car1.Speed = 50;
            car1.IsBreaking = true;
            car1.Drive(2000);
            Assert.AreEqual(car1.Coordinate, new Point(818, 50));
            Assert.AreEqual(car1.PictureBox.Location, new Point(818, 50));
        }
    }
}
