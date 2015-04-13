using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Util.Collections
{
    public class PriorityTreeNode<K, V> where K : IComparable<K>
    {
        public List<V> Values { get; set; }
        public K Key { get; set; }

        public PriorityTreeNode<K, V> Left { get; set; }
        public PriorityTreeNode<K, V> Right { get; set; }

        public PriorityTreeNode()
        {
            Values = new List<V>();
            Left = Right = null;
        }

        /// <summary>
        /// Creates a new Node
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="values">A list of values for that particular key</param>
        public PriorityTreeNode(K key, List<V> values)
        {
            Key = key;
            Values = values;
            Left = Right = null;
        }

        /// <summary>
        /// Creates a new Node
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">A single value for that particular key</param>
        public PriorityTreeNode(K key, V value) : this(key, new List<V> { value }) { }

        public override string ToString()
        {
            var current = Key.ToString();
            var left = Left != null ? Left.ToString() : "";
            var right = Right != null ? Right.ToString() : "";

            return current + "(" + left + ", " + right + ")";
        }
    }
}
