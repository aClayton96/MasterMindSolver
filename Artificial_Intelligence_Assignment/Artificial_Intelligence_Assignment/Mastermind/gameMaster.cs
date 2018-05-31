using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artificial_Intelligence_Assignment.CodeBreakers;
using Artificial_Intelligence_Assignment.Display;
using Artificial_Intelligence_Assignment.Mastermind;

namespace Artificial_Intelligence_Assignment.Mastermind
{
    class gameMaster
    {
        comparisonEngine cE = new comparisonEngine();

        private bool win = false;
        private int tries = 0;

        public bool m_win { get { return win; } set { win = value; } }

        public int m_tries { get { return tries; } set { tries = value; } }

        //Game code array
        private static char[] gameCode = new char[4];

        //Getter and setter
        public static char[] m_gameCode { get { return gameCode; } }

        bool initialGuess = false;

        char[] guess = new char[4];
        char[] _feedback = new char[4];

        //Constructor
        public gameMaster()
        {
            //Generating the "Winning code"
            string chars = "123456";
            Random rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                int num = rand.Next(0, chars.Length - 1);
                gameCode[i] = chars[num];
            }
        }
        display d = new display(m_gameCode);

        /// <summary>
        /// Human player for testing and evidencing working "mastermind" game
        /// </summary>
        public void humanPlay()
        {
            player p = new player();

            bool running = true;

            while (running)
            {
                if (!win)
                {
                    if (tries < 10)
                    {
                        char[] guess = p.guess();
                        cE.compare(guess, m_gameCode, out win, m_tries, d);
                        tries++;
                        d.update();
                    }
                    else
                    {
                        Console.WriteLine("Game over");
                        running = false;
                    }
                }
                else
                {
                    Console.WriteLine("You win");
                    running = false;
                }
            }
        }

        /// <summary>
        /// Knuth's Five Guess Algorithm solver or Genetic Algorithm solver
        /// </summary>
        public void aiPlay(char c)
        {
            fiveGuess fG = new fiveGuess();
            geneticSolver gS = new geneticSolver();

            while (!win && tries < 10)
            {

                if (initialGuess == false)
                {
                    switch (c) {
                        case '2':
                            guess = fG.guess(null, initialGuess);
                            break;
                        case '3':
                            guess = gS.guess(null, initialGuess);
                            break;
                    }
                    initialGuess = true;
                }
                else if (initialGuess == true)
                {
                    switch(c)
                    {
                        case '2':
                            guess = fG.guess(feedbackComparable(_feedback), initialGuess);
                            break;

                        case '3':
                            guess = gS.guess(feedbackComparable(_feedback), initialGuess);
                            break;
                    }
                }

                _feedback = cE.compare(guess, m_gameCode, out win, m_tries, d);

                tries++;

                d.update();
            }
            if (!win && tries >= 10)
            {
                Console.WriteLine("Game over");
            }
            else if (win && tries < 10)
            {
                Console.WriteLine("You win");
            }
        }

        /// <summary>
        /// Turns Character array feedback (used for displaying to the screen) into outcomes (Used in both five guess and genetic solvers)
        /// </summary>
        /// <param name="_feedback">Char array feedback</param>
        /// <returns></returns>
        private outcome feedbackComparable(char[] _feedback) 
        {
            outcome Comparablefeedback = new outcome();

            int whites = 0;

            int blacks = 0;

            if (_feedback != null)
            {
                foreach (char c in _feedback)
                {
                    if (c == 'W')
                    {
                        whites++;
                    }
                    else if (c == 'B')
                    {
                        blacks++;
                    }
                }

                while (blacks > 0)
                {
                    Comparablefeedback.m_black++;
                    blacks--;
                }
                while (whites > 0)
                {
                    Comparablefeedback.m_white++;
                    whites--;
                }

                return Comparablefeedback;
            }
            else
            {
                return null;
            }
        }
    }
}
