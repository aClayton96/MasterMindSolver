using Artificial_Intelligence_Assignment.Mastermind;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence_Assignment.CodeBreakers
{
    class ai_Solver
    {
        private List<char[]> combinations = new List<char[]>();

        private List<char[]> guesses = new List<char[]>();

        private char[] prevGuess = new char[4];

        public List<char[]> m_combinations { get { return combinations; } }

        public List<char[]> m_guesses {  get { return guesses; } set { guesses = value; } }

        public char[] m_prevGuess { get { return prevGuess; } set { prevGuess = value; } }

        public char[] guess()
        {
            return null;
        }
        /// <summary>
        /// Generate all possible game code combinations
        /// </summary>
        public void generateCombinations()
        {
            for (int a = 1; a < 7; a++)
            {
                for (int b = 1; b < 7; b++)
                {
                    for (int x = 1; x < 7; x++)
                    {
                        for (int y = 1; y < 7; y++)
                        {
                            char[] g = new char[4];

                            g[0] = Convert.ToChar(a.ToString());
                            g[1] = Convert.ToChar(b.ToString());
                            g[2] = Convert.ToChar(x.ToString());
                            g[3] = Convert.ToChar(y.ToString());

                            combinations.Add(g);
                            guesses.Add(g);

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Compare the guess made with the winning code and generate feedback
        /// </summary>
        /// <param name="_guess">The guess made</param>
        /// <param name="_solution">The winning code</param>
        /// <returns></returns>
        public outcome check(char[] _guess, char[] _solution)
        {
            char[] tempCode = new char[4];

            char[] tempGuess = new char[4];

            for (int i = 0; i < 4; i++)
            {
                tempCode[i] = _solution[i];
            }

            for (int i = 0; i < 4; i++)
            {
                tempGuess[i] = _guess[i];
            }

            outcome o = new outcome();

            //Number of black and white pins
            int white = 0;
            int black = 0;

            //Iterate through the code and guess arrays to find matching pairs
            for (int i = 0; i < 4; i++)
            {
                if (tempCode[i] == tempGuess[i])
                {
                    //change the values so they won't match anymore
                    tempCode[i] = '-';
                    tempGuess[i] = '.';

                    //Add one to the number of white pins
                    black++;
                }
            }

            //Iterate through the remaining values and 
            foreach (char c in tempGuess)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (tempCode[i] == c)
                    {
                        tempCode[i] = '-';
                        //Add one to the number of black pins
                        white++;
                        break;
                    }
                }
            }

            while (black > 0)
            {
                o.m_black++;
                black--;
            }
            while (white > 0)
            {
                o.m_white++;
                white--;
            }
            return o;
        }
    }
}
