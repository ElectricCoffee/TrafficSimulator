using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficSim.Util;

namespace UnitTests
{
    [TestClass]
    public class ErrorLogTest
    {
        // creates a path to a file called "error-log.txt-test" on the desktop
        string testErrorPath = Error.DesktopPath + "-test"; 

        /// <summary>
        /// Removes the file created by the test 
        /// </summary>
        [TestCleanup]
        public void RemoveAllFiles()
        {
            if (System.IO.File.Exists(testErrorPath))
            {
                System.IO.File.Delete(testErrorPath); 
            }
        }

        [TestMethod]
        public void TestStringLogging()
        {
            string expected = "This is a test of an error";
            Error.Log(expected, testErrorPath);
            string actual = Error.ReadLog(testErrorPath);

            StringAssert.Equals(expected + "\n", actual);
        }

        [TestMethod]
        public void TestExceptionLogging()
        {
            var expected = new Exception("This is an exception test");
            Error.Log(expected, testErrorPath);
            string actual = Error.ReadLog(testErrorPath);

            StringAssert.Equals(expected.Message + "\n", actual);
        }
    }
}
