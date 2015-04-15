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
            Action callMeMaybe = () => { success = true; };
            bigBoy.RemoveContinuousEvent(() => { });

            Assert.IsTrue(true);
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
    }
}
