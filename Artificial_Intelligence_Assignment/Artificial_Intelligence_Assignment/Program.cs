using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artificial_Intelligence_Assignment.Mastermind;

namespace Artificial_Intelligence_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            menuSystem mS = new menuSystem();

            mS.runMenu();

            Console.ReadLine();
        }
    }
}
