using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Restrictions : Sign
    {
        string Restriction;

        public override string RoadRestriction()
        {
            return Restriction;
        }
    }
}
