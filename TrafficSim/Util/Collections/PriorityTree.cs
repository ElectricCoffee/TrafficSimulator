using System;
using System.Collections.Generic;

namespace TrafficSim.Util.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class PriorityTree<K, V> where K : IComparable, IComparable<K>
    {
        private PriorityTreeNode<K, V> root = null;

        /// <summary>
        /// Create a new PriorityTree with a node
        /// </summary>
        private PriorityTree()
        {
            root = new PriorityTreeNode<K,V>();
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

        public void Add(K key, V value)
        {
            Add(root, key, value);
        }

        private void Add(PriorityTreeNode<K, V> node, K key, V value)
        {
            if (node == null) throw new NullReferenceException("input-node was null");
            // if the key is larger, go down the right node
            if (node.Key.CompareTo(key) < 0) 
            {
                if (node.Right == null)
                    node.Right = new PriorityTreeNode<K,V>(key, value);
                
                else Add(node.Right, key, value);
            }
            // if the key is smaller, go down the left node
            else if (node.Key.CompareTo(key) > 0) {
                if (node.Left == null)
                    node.Left = new PriorityTreeNode<K,V>(key, value);

                else Add(node.Left, key, value);
            }
            // if the key is identical, add it to the list of keys
            else node.Values.Add(value); 
        }

        public List<V> GetSmallest()
        {
            var temp = root;

            while (temp.Left != null)
                temp = temp.Left;

            var res = temp.Values;
            temp = temp.Right;

            return res;
        }

#warning not tested, may fail
        public List<V> GetLargest()
        {
            var temp = root;

            while (temp.Right != null)
                temp = temp.Right;

            var res = temp.Values;
            temp = temp.Left;

            return res;
        }

    }
}
