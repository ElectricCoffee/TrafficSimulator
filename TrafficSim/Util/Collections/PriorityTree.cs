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
            Add(root, key, value);
        }

        private void Add(PriorityTreeNode<K, V> node, K key, V value)
        {
            if (node == null) node = new PriorityTreeNode<K, V>(key, value);
            else
            {
                if (node.Key.CompareTo(key) > 0)
                    Add(node.Right, key, value);
                else if (node.Key.CompareTo(key) < 0)
                    Add(node.Left, key, value);
                else node.Values.Add(value);
            }
        }

#error Incomplete, do not attempt to use
        public List<V> GetSmallest()
        {
            var temp = root;

            while (temp.Left != null)
            {
                temp = temp.Left;
            }

            var res = temp.Values;

            return res;
        }
#error Incomplete, do not attempt to use
        public List<V> GetLargest()
        {
            return null;
        }
    }
}
