using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artificial_Intelligence_Assignment.Mastermind;

namespace Artificial_Intelligence_Assignment.Display
{
    class display
    {
        private char[,] grid = new char[14, 11];

        private char[] gameCode = new char[4];

        public char[,] _grid
        {
            get { return grid; }
            set { grid = value; }
        }

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="_gameCode">Takes the "winning" code to output to the display (for AI debug)</param>
        public display(char[] _gameCode)
        {
            gameCode = _gameCode;
            Initialise();
        }

        /// <summary>
        /// Initialise a grid
        /// </summary>
        public void Initialise()
        {
            for (int y = 0; y < 14; y++)
            {
                for (int x = 0; x < 11; x++)
                {
                    if (y == 0 || y == 13)
                    {
                        grid[y, x] = '-';
                    }
                    else if (x == 0 || x == 5 || x == 10)
                    {
                        grid[y, x] = '|';
                    }
                    else
                    {
                        grid[y, x] = '.';
                    }
                }
            }
            update();
        }

        /// <summary>
        /// Update the grid with each guess
        /// </summary>
        public void update()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Mastermind!");
            Console.WriteLine("   " + "Guess Board" + "   " + "Feedback Board" + "   " + "Winning code: " + gameCode[0] + gameCode[1] + gameCode[2] + gameCode[3]);
            for (int y = 0; y < 14; y++)
            {
                for (int x = 0; x < 11; x++)
                {
                    Console.Write(" " + grid[y, x] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
