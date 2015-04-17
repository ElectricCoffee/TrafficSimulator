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
        public void DrawTest()
        {
            Car car1 = new Car(50, 50);
            car1.Draw();
            Assert.AreEqual(car1.PictureBox.Location, new Point(50, 50));
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
            //how to get rotation?
#warning           Assert.AreEqual(, RotateFlipType.Rotate270FlipNone); //how do I get the rotation of the image?
        }

        [TestMethod]
        public void AccelerationTest() 
        {
            Car car1 = new Car(50, 50);
            car1.Direction = new Point(50, 0);
            car1.Acc = 1;
            car1.Accelerate(2000);
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
            car1.Accelerate(2000);
            Assert.AreEqual(car1.Coordinate, new Point(818, 50));
            Assert.AreEqual(car1.PictureBox.Location, new Point(818, 50));
        }
    }
}
