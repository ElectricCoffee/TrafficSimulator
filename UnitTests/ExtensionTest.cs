using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;
using TrafficSim.Util.Extensions;

namespace UnitTests
{
    [TestClass]
    public class ExtensionTest
    {
        [TestMethod]
        public void DotProductTest()
        {
            var a = new Point(2, 4);
            var b = new Point(3, 5);
            var expected = 2 * 3 + 4 * 5;
            var actual = a.Dot(b);

            Assert.AreEqual(expected, actual);
        }
    }
}
