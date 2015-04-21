using System;
using System.Collections.Generic;
using System.Globalization;

namespace TrafficSim.Util.Collections
{
    /// <summary>
    /// A priority collection that works in the style of a queue.
    /// Values can be pushed in, and popped out as either greatest or smallest value
    /// </summary>
    /// <typeparam name="K">The key type, that must be comparable</typeparam>
    /// <typeparam name="V">The value type, that can be anything</typeparam>
    public class PriorityTree<K, V> where K : IComparable, IComparable<K>
    {
        private PriorityTreeNode<K, V> root = null;

        public bool IsEmpty { get { return root == null; } }

        /// <summary>
        /// Create a new PriorityTree with a node
        /// </summary>
        public PriorityTree()
        {
            root = null;
        }

        /// <summary>
        /// Creates a new PriorityTree with a node containing a key and a value
        /// </summary>
        /// <param name="key">The key, used to index it</param>
        /// <param name="value">The value</param>
        public PriorityTree(K key, V value)
        {
            root = new PriorityTreeNode<K, V>(key, value);
        }

        /// <summary>
        /// Pushes data onto the tree
        /// </summary>
        /// <param name="key">The key the tree orders by</param>
        /// <param name="value">The value stored with the key</param>
        /// <param name="overwrite">Decides whether or not you want to overwrite the value in case of duplicate keys</param>
        public void Push(K key, V value, bool overwrite = false)
        {
            if (IsEmpty)
                root = new PriorityTreeNode<K, V>(key, value);
            else 
                Push(root, key, value, overwrite);
        }

        /// <summary>
        /// Pushes data onto a node
        /// </summary>
        /// <param name="node">The specific node you want to push to</param>
        /// <param name="key">The key the tree orders by</param>
        /// <param name="value">The value stored with the key</param>
        /// <param name="overwrite">Decides whether or not you want to overwrite the value in case of duplicate keys</param>
        public void Push(PriorityTreeNode<K, V> node, K key, V value, bool overwrite = false)
        {
            if (node == null) throw new NullReferenceException("input-node was null");
            // if the key is larger, go down the right node

            int comparison = 0;
            bool isGreater, isLesser;

            // trying to get rid of culture info, it messes up the comparison
            if (node.Key is String)
            {
                var nodeKey = node.Key as String;
                var localKey = key as String;

                // may or may not be a good thing to do
                comparison = String.Compare(nodeKey, localKey, ignoreCase: false, culture: CultureInfo.InvariantCulture);
            }
            else comparison = node.Key.CompareTo(key);

            if (comparison < 0) // yes the < needs to point this way, blame the design of C#, don't fix it
            {
                if (node.Right == null)
                    node.Right = new PriorityTreeNode<K,V>(key, value);
                
                else Push(node.Right, key, value, overwrite);
            }
            // if the key is smaller, go down the left node
            else if (comparison > 0)
            {
                if (node.Left == null)
                    node.Left = new PriorityTreeNode<K, V>(key, value);

                else Push(node.Left, key, value, overwrite);
            }
            // if the key is identical, add it to the list of keys
            else
            {
                if (overwrite)
                    node.Values = new List<V> { value };
                else
                    node.Values.Add(value);
            }
        }

        /// <summary>
        /// Helper function to reduce repeated pattern of iterating the tree
        /// </summary>
        /// <param name="nextNode">A function that takes a PriorityTreeNode, and returns a PriorityTreeNode</param>
        /// <returns></returns>
        private PriorityTreeNode<K, V> IterateTree(Func<PriorityTreeNode<K, V>, PriorityTreeNode<K, V>> nextNode)
        {
            if (IsEmpty) throw new TreeEmptyException("Trying to iterate empty tree!");

            var temp = root;

            while (nextNode(temp) != null)
                temp = nextNode(temp);

            return temp;
        }

        /// <summary>
        /// Removes and returns the value stored on the 'smallest' key
        /// </summary>
        /// <returns></returns>
        public List<V> PopSmallest()
        {
            var temp = IterateTree(x => x.Left);

            var res = temp.Values;

            temp = temp.Right;

            return res;
        }

        /// <summary>
        /// Returns the value stored on the 'smallest' key without removing it.
        /// </summary>
        /// <returns></returns>
        public List<V> PeekSmallest()
        {
            return IterateTree(x => x.Left).Values;
        }

        /// <summary>
        /// Removes and returns the value stored on the 'largest' key
        /// </summary>
        /// <returns></returns>
        public List<V> PopLargest()
        {
            var temp = IterateTree(x => x.Right);

            var res = temp.Values;

            temp = temp.Left;

            return res;
        }

        /// <summary>
        /// Returns the value stored on the 'largest' key without removing it.
        /// </summary>
        /// <returns></returns>
        public List<V> PeekLargest()
        {
            return IterateTree(x => x.Right).Values;
        }

    }
}
