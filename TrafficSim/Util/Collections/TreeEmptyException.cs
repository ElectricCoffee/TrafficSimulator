﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Util.Collections
{
    /// <summary>
    /// Specific exception for when the tree is empty
    /// </summary>
    public class TreeEmptyException : Exception
    {
        public TreeEmptyException() : base("The tree was empty") { }
        public TreeEmptyException(string message) : base(message) {}
        public TreeEmptyException(string message, Exception inner) : base(message, inner) { }
    }
}
