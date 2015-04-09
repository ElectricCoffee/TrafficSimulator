using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Util.Collections
{
    public class PriorityTreeNode<K, V>
    {

        public PriorityTreeNode(K key, List<V> values)
        {
            Key = key;
            Values = values;
            Left = Right = null;
        }

        public PriorityTreeNode(K key, V value) : this(key, new List<V> { value }) { }

        public List<V> Values { get; set; }
        public K Key { get; set; }

        public PriorityTreeNode<K, V> Left { get; set; }
        public PriorityTreeNode<K, V> Right { get; set; }
    }
}
