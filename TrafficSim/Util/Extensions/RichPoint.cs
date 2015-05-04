using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Util.Extensions
{
    public static class RichPoint
    {
        /// <summary>
        /// Takes the dot-product of two points
        /// </summary>
        /// <param name="left">the point to the left of the "dot"</param>
        /// <param name="right">the point to the right of the "dot"</param>
        /// <returns>Returns a scalar value representing the product</returns>
        public static Int32 Dot(this System.Drawing.Point left, System.Drawing.Point right)
        {
            return left.X * right.X + left.Y * right.Y;
        }
    }
}
