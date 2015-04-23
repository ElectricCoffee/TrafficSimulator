using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficSim.Event;

namespace UnitTests
{

    [TestClass]
    public class EventHandlerTest
    {
        public class TestCallbackHolder : ISimulatable
        {
            public TestCallbackHolder(TrafficEventHandler initEventHandler)
            {
                eventHandler = initEventHandler;
                eventHandler.AddDiscreteEvent(callMeMaybe, TimeSpan.FromMilliseconds(100));
                eventHandler.AddContinuousEvent(callMeMaybe);
                usedMethod = false;
            }
            public bool usedMethod { get; private set; }
            public TrafficEventHandler eventHandler {get; set;}
            void callMeMaybe()
            {
                usedMethod = true;
            }
        }

        public TrafficEventHandler testEventHandler;

        [TestInitialize]
        public void TestInialize()
        {
            testEventHandler = new TrafficEventHandler(TimeSpan.FromMilliseconds(100));
        }

        /// <summary>
        /// Tries to removes a callback that isn't in the callback-list.
        /// Expected: No callbacks are removed, no exceptions are thrown.
        /// </summary>
        [TestMethod]
        public void RemoveFakeContinuousEvent()
        {
            testEventHandler.RemoveContinuousEvent(() => { });

            Assert.IsTrue(true); //It passes with no exceptions thrown
        }
        /// <summary>
        /// Tries to removes a callback that is in the callback-list.
        /// Expected: Callback isn't triggered with NextTick(), success stays true.
        /// </summary>
        [TestMethod]
        public void RemoveRealContinuousEvent()
        {
            bool success = true;

            Action callMeMaybe = () => { success = false; };
            testEventHandler.AddContinuousEvent(callMeMaybe);

            testEventHandler.RemoveContinuousEvent(callMeMaybe);
            testEventHandler.NextTick();

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

            Action callMeMaybe = () => { actual += 1; };
            testEventHandler.AddContinuousEvent(callMeMaybe);

            for (int ticks = 0; ticks < 3; ticks++)
            {
                testEventHandler.NextTick();
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

            Action callMeMaybe = () => { actual += 1; };

            testEventHandler.AddContinuousEvent(callMeMaybe);
            testEventHandler.NextTick();
            testEventHandler.NextTick();
            testEventHandler.ClearContinuousEvents();
            testEventHandler.NextTick();

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

            testEventHandler.AddDiscreteEvent(callMeMaybe, TimeSpan.FromSeconds(2));
            for (int ticks = 0; ticks < 19; ticks++) testEventHandler.NextTick();
            Assert.IsFalse(success);
            testEventHandler.NextTick();
            Assert.IsTrue(success);
        }
        /// <summary>
        /// Clears all discrete events before they are called.
        /// Expected: callMeMaybe isn't called, success stays true.
        /// </summary>
        [TestMethod]
        public void TestClearDiscreteEvents()
        {
            bool success = true;

            Action callMeMaybe = () => { success = false; };

            testEventHandler.AddDiscreteEvent(callMeMaybe, TimeSpan.FromSeconds(1));
            testEventHandler.AddDiscreteEvent(callMeMaybe, TimeSpan.FromSeconds(2));
            testEventHandler.ClearDiscreteEvents();
            for (int ticks = 0; ticks < 30; ticks++)
            {
                testEventHandler.NextTick();
            }

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

            Action callMeYes = () => { actual += 3; };
            Action callMeNo = () => { actual -= 1; };

            testEventHandler.AddContinuousEvent(callMeYes);
            testEventHandler.AddContinuousEvent(callMeNo);
            for (int ticks = 0; ticks < 3; ticks++)
            {
                testEventHandler.NextTick();
            }

            Assert.AreEqual(actual, expected);
        }
        /// <summary>
        /// Calls an event from an object, that is added during object construction.
        /// Expected: callbackHolder.usedMethod becomes true.
        /// </summary>
        [TestMethod]
        public void CallFromObject()
        {
          var callbackHolder = new TestCallbackHolder(testEventHandler);

          testEventHandler.NextTick();

          Assert.IsTrue(callbackHolder.usedMethod);
        }
        /// <summary>
        /// Clears all events from an object. (The one that is added during object construction)
        /// Expected: callbackHolder.usedMethod stays false, the event is cleared before callback.
        /// </summary>
        [TestMethod]
        public void ClearCallsFromObject()
        {
          var callbackHolder = new TestCallbackHolder(testEventHandler);

          testEventHandler.ClearEventsFromObject(callbackHolder);
          testEventHandler.NextTick();

          Assert.IsFalse(callbackHolder.usedMethod);
        }
    }
}