using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Add(key, value);
        }

        public void Add(K key, V value)
        {
            var temp = root;

            if (temp == null)
                temp = new PriorityTreeNode<K, V>(key, value);
            else if (temp.Key.CompareTo(key) > 0)
                temp.Right = new PriorityTreeNode<K, V>(key, value);
            else if (temp.Key.CompareTo(key) < 0)
                temp.Left = new PriorityTreeNode<K, V>(key, value);
            else temp.Values.Add(value); 
        }
    }
}
