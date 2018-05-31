using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence_Assignment.Mastermind
{
    class menuSystem
    {
        gameMaster gM = new gameMaster();

        /// <summary>
        /// The main menu method
        /// 
        /// </summary>
        public void runMenu()
        {
            Console.WriteLine("Welcome to MasterMind");

            Console.WriteLine("Please select an option");

            Console.WriteLine("1: Human Player Game");

            Console.WriteLine("2: Minimax");

            Console.WriteLine("3: Genetic");

            bool isValid = false;

            while (isValid == false)
            {

                string s = Console.ReadLine();

                if (validFormat(s) == true)
                {
                    char c = s[0];
                    isValid = true;
                    switch (c)
                    {
                        case '1':

                            gM.humanPlay();
                            break;

                        case '2':
                        case '3':

                            gM.aiPlay(c);
                            break;
                    }
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Please enter a valid option");
                }
            }
        }

        /// <summary>
        /// Simple input validation
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        bool validFormat(string format)
        {
            string allowable = "123";

            if (!allowable.Contains(format) || format.Length > 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
