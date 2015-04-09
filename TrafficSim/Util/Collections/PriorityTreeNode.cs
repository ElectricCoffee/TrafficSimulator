using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Util.Collections
{
    private class PriorityTreeNode<K, V> where K : IComparable<K>
    {
        public List<V> Values { get; set; }
        public K Key { get; set; }

        public PriorityTreeNode<K, V> Left { get; set; }
        public PriorityTreeNode<K, V> Right { get; set; }
        public PriorityTreeNode(K key, List<V> values)
        {
            Key = key;
            Values = values;
            Left = Right = null;
        }

        public PriorityTreeNode(K key, V value) : this(key, new List<V> { value }) { }
    }
}
