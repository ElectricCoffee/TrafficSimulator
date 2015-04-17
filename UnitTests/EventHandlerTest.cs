using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficSim.Event;

namespace UnitTests
{
    public class TestCallbackHolder {
        public TestCallbackHolder(TrafficEventHandler handler)
        {
            handler.AddDiscreteEvent(callMeMaybe,TimeSpan.FromMilliseconds(100));
            handler.AddContinuousEvent(callMeMaybe);
            usedMethod = false;
        }
        public bool usedMethod { get; private set; }
        void callMeMaybe () {
            usedMethod = true;
        }
    }

    [TestClass]
    public class EventHandlerTest
    {
        //The following tests if the public methods will complete.
        //Expected: No exceptions thrown
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
        /// <summary>
        /// Tries to removes a callback that isn't in the callback-list.
        /// Expected: No callbacks are removed, no exceptions are thrown.
        /// </summary>
        [TestMethod]
        public void RemoveFakeContinuousEvent()
        {
            new TrafficEventHandler(TimeSpan.FromMilliseconds(100)).RemoveContinuousEvent(() => { });

            Assert.IsTrue(true);
        }
        /// <summary>
        /// Tries to removes a callback that is in the callback-list.
        /// Expected: Callback isn't triggered with NextTick(), success stays true.
        /// </summary>
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
        /// <summary>
        /// Calls a continuous event through 3 ticks.
        /// Expected: Actual is incremented each tick, to the expected 3.
        /// </summary>
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
        /// <summary>
        /// Same as previous, but clears continuous events after the second tick.
        /// Expected: Actual is only incremented two ticks, to the expected 2.
        /// </summary>
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
        /// <summary>
        /// Calls a discrete event after 2 seconds.
        /// Expected: Ticks through 2 seconds, changing success to true at 2 seconds.
        /// </summary>
        [TestMethod]
        public void CallDiscreteEvent()
        {
            bool success = false;
            Action callMeMaybe = () => { success = true; };
            var bigBoy = new TrafficEventHandler(TimeSpan.FromMilliseconds(100));

            bigBoy.AddDiscreteEvent(callMeMaybe, TimeSpan.FromSeconds(2));
            for (int ticks = 0; ticks < 19; ticks++)
            {
                bigBoy.NextTick();
            }
            Assert.IsFalse(success);
            bigBoy.NextTick();
            Assert.IsTrue(success);
        }
        /// <summary>
        /// Adds two continuous events to the callback-list
        /// Expected: After 3 ticks the combined actual becomes 3(3-1) = 6
        /// </summary>
        [TestMethod]
        public void CallMultipleContinuousEvents()
        {
            int actual = 0;
            int expected = 6;


            var bigBoy = new TrafficEventHandler(TimeSpan.FromMilliseconds(100));
            Action callMeYes = () => { actual += 3; };
            Action callMeNo = () => { actual -= 1; };
            bigBoy.AddContinuousEvent(callMeYes);
            bigBoy.AddContinuousEvent(callMeNo);
            for (int ticks = 0; ticks < 3; ticks++)
            {
                bigBoy.NextTick();
            }

            Assert.AreEqual(actual, expected);
        }
        /// <summary>
        /// Calls an event from an object, that is added during object construction.
        /// Expected: beholder.usedMethod becomes true.
        /// </summary>
        [TestMethod]
        public void CallFromObject()
        {
          var bigBoy = new TrafficEventHandler(TimeSpan.FromMilliseconds(100));
          var beholder = new TestCallbackHolder(bigBoy);

          bigBoy.NextTick();

          Assert.IsTrue(beholder.usedMethod);
        }
        /// <summary>
        /// Clears all events from an object. (The one that is added during object construction)
        /// Expected: beholder.usedMethod stays false, the event is cleared before callback.
        /// </summary>
        [TestMethod]
        public void ClearCallsFromObject()
        {
          var bigBoy = new TrafficEventHandler(TimeSpan.FromMilliseconds(100));
          var beholder = new TestCallbackHolder(bigBoy);

          bigBoy.ClearEventsFromObject(beholder);
          bigBoy.NextTick();

          Assert.IsFalse(beholder.usedMethod);
        }
    }
}
