using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence_Assignment.Mastermind
{
    class outcome
    {
        private int white;
        private int black;

        public int m_white { get { return white; } set { white = value; } }
        public int m_black { get { return black; } set { black = value; } }


        //Overrides for outcome comparisons
        public static bool operator ==(outcome a, outcome b)
        {
            return a.white == b.white && a.black == b.black;
        }
        public static bool operator !=(outcome a, outcome b)
        {
            return a.white != b.white && a.black != b.black;
        }
    }
}
