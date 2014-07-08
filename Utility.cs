using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionBot
{
    class Utility
    {
        public static int randomInt(Random rand, int incLB, int exclUB)
        {
            return rand.Next(incLB, exclUB);
        }
    }
}
