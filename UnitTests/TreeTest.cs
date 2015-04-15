using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrafficSim.Util.Collections;

namespace UnitTests
{
    [TestClass]
    public class TreeTest
    {
        [TestMethod]
        public void TestPeek()
        {
            var tree = new PriorityTree<string, int>();

            tree.Push("hello", 12);
            tree.Push("guten tag", 45);
            tree.Push("aaa", 0);
            tree.Push("zachary", 99);

            var smallest = tree.PeekSmallest()[0];
            var expectedSmallest = 0;
            var largest = tree.PeekLargest()[0];
            var expectedLargest = 99;

            Assert.AreEqual(expectedSmallest, smallest);
            Assert.AreEqual(expectedLargest, largest);
        }

        [TestMethod]
        [ExpectedException(typeof(TreeEmptyException))]
        public void EmptyPop()
        {
            var tree = new PriorityTree<int, int>();
            tree.PopLargest();
        }

        [TestMethod]
        public void EmptyCheck()
        {
            var tree = new PriorityTree<int, int>();
            Assert.IsTrue(tree.IsEmpty);
        }
    }
}
