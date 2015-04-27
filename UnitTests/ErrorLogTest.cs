using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficSim.Util;

namespace UnitTests
{
    [TestClass]
    public class ErrorLogTest
    {
        [TestCleanup]
        public void RemoveAllFiles()
        {
            if (System.IO.File.Exists(Error.DesktopPath))
            {
                System.IO.File.Delete(Error.DesktopPath); 
            }
        }

        [TestMethod]
        public void TestStringLogging()
        {
            string expected = "This is a test of an error";
            Error.Log(expected);
            string actual = Error.ReadLog();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestExceptionLogging()
        {
            var expected = new Exception("This is an exception test");
            Error.Log(expected);
            string actual = Error.ReadLog();
            System.IO.File.Delete(Error.DesktopPath);

            Assert.AreEqual(expected.Message, actual);
        }
    }
}
