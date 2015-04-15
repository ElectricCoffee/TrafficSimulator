using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficSim.Event;

namespace UnitTests
{
    [TestClass]
    public class EventHandlerTest
    {
        [TestMethod]
        public void RunNextTick()
        {
            new TrafficEventHandler(TimeSpan.FromMilliseconds(100)).NextTick();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void RunAddDiscreteEvent()
        {
            new TrafficEventHandler(TimeSpan.FromMilliseconds(100)).AddDiscreteEvent(() => { }, TimeSpan.FromSeconds(2));

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void RunAddContinuousEvent()
        {
            new TrafficEventHandler(TimeSpan.FromMilliseconds(100)).AddContinuousEvent(() => { });

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void RunClearEventsFromFakeObject()
        {
            new TrafficEventHandler(TimeSpan.FromMilliseconds(100)).ClearEventsFromObject(new {});

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void RunClearContinuousEvents()
        {
            new TrafficEventHandler(TimeSpan.FromMilliseconds(100)).ClearContinuousEvents();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void RemoveFakeContinuousEvent()
        {
            new TrafficEventHandler(TimeSpan.FromMilliseconds(100)).RemoveContinuousEvent(() => { });

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void RemoveRealContinuousEvent()
        {
            bool success = true;

            var bigBoy = new TrafficEventHandler(TimeSpan.FromMilliseconds(100));
            Action callMeMaybe = () => { success = false; };
            bigBoy.AddContinuousEvent(callMeMaybe);
            bigBoy.RemoveContinuousEvent(callMeMaybe);
            bigBoy.NextTick();

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void CallContinuousEvent()
        {
            int actual = 0;
            int expected = 3;


            var bigBoy = new TrafficEventHandler(TimeSpan.FromMilliseconds(100));
            Action callMeMaybe = () => { actual += 1; };
            bigBoy.AddContinuousEvent(callMeMaybe);
            for (int ticks = 0; ticks < 3; ticks++)
            {
                bigBoy.NextTick();
            }

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TestClearContinuousEvents()
        {
            int actual = 0;
            int expected = 2;


            var bigBoy = new TrafficEventHandler(TimeSpan.FromMilliseconds(100));
            Action callMeMaybe = () => { actual += 1; };
            bigBoy.AddContinuousEvent(callMeMaybe);
            bigBoy.NextTick();
            bigBoy.NextTick();
            bigBoy.ClearContinuousEvents();
            bigBoy.NextTick();

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void CallDiscreteEvent()
        {
            bool success = false;
            Action callMeMaybe = () => { success = true; };
            var bigBoy = new TrafficEventHandler(TimeSpan.FromMilliseconds(100));

            bigBoy.AddDiscreteEvent(callMeMaybe, TimeSpan.FromSeconds(2));
            for (int ticks = 0; ticks < 30; ticks++)
            {
                bigBoy.NextTick();
            }

            Assert.IsTrue(success);
        }

        [TestMethod]
        public void CallMultipleContinuousEvents()
        {
#error Not yet done!
            int actual = 0;
            int expected = 6;


            var bigBoy = new TrafficEventHandler(TimeSpan.FromMilliseconds(100));
            Action callMeMaybe = () => {actual += 1; };
            bigBoy.AddContinuousEvent(callMeMaybe);
            for (int ticks = 0; ticks < 3; ticks++)
            {
                bigBoy.NextTick();
            }

            Assert.AreEqual(actual, expected);
        }
    }
}
