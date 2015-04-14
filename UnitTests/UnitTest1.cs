using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Example1()
        {
            // this is an example test, just to show you guys how things work:

            int myCalculation = 45 + 6; // complicated, right?
            int myExpectedRestult = 51;

            // the left argument is what you expect to happen, on the right is what you need to get the restult from
            Assert.AreEqual(myExpectedRestult, myCalculation);

            // please add a new test method for each test you need to run, this makes debugging easier
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))] // here's what you do when you want something to return an exception
        public void Example2()
        {
            string value = null;
            value.CompareTo("this will fail, as intended");

            // Note that you can benefit from enabling the Test Explorer from TEST -> Windows -> Test Explorer
        }
    }
}
