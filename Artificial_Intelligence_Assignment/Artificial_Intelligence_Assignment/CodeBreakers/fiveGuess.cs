using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artificial_Intelligence_Assignment.Mastermind;

namespace Artificial_Intelligence_Assignment.CodeBreakers
{
    class fiveGuess : ai_Solver
    {
        public fiveGuess()
        {
            generateCombinations();
        }

        /// <summary>
        /// Call various methods to generate and return a guess.
        /// </summary>
        /// <param name="_feedback">Previous guess feedback</param>
        /// <param name="_initialGuess">An initial guess boolean to check if first guess</param>
        /// <returns></returns>
        public char[] guess(outcome _feedback, bool _initialGuess)
        {
            outcome[] outcomes = new[] { new outcome { m_white = 0, m_black = 0 },
                                         new outcome { m_white = 0, m_black = 1 },
                                         new outcome { m_white = 0, m_black = 2 },
                                         new outcome { m_white = 0, m_black = 3 },
                                         new outcome { m_white = 0, m_black = 4 },
                                         new outcome { m_white = 1, m_black = 0 },
                                         new outcome { m_white = 1, m_black = 1 },
                                         new outcome { m_white = 1, m_black = 2 },
                                         new outcome { m_white = 1, m_black = 3 },
                                         new outcome { m_white = 2, m_black = 0 },
                                         new outcome { m_white = 2, m_black = 1 },
                                         new outcome { m_white = 2, m_black = 2 },
                                         new outcome { m_white = 3, m_black = 0 },
                                         new outcome { m_white = 4, m_black = 0 }
                                       };

            if (_initialGuess == false)
            {
                char[] initialguess = new char[4];
                initialguess[0] = '1';
                initialguess[1] = '1';
                initialguess[2] = '2';
                initialguess[3] = '2';

                m_prevGuess = initialguess;

                return initialguess;
            }
            else
            {
                m_guesses = elimination(outcomes, m_guesses, _feedback, m_prevGuess); //Eliminate guesses based on the feedback from the previous guess
                return applyMinMax(outcomes, m_combinations, m_guesses, _feedback); //Use Knuths algorithm to decide the next guess
            }
        }

        /// <summary>
        /// Apply a minmax method to decide on a guess from the list of possible guesses.
        /// </summary>
        /// <param name="_outcomes">An array of all possible outcomes</param>
        /// <param name="_combinations">All possible combinations</param>
        /// <param name="_guesses">Current eligible guesses</param>
        /// <param name="_feedback">The previous guess feedback</param>
        /// <returns></returns>
        private char[] applyMinMax(outcome[] _outcomes, List<char[]> _combinations, List<char[]> _guesses, outcome _feedback)
        {
            
            int min = int.MaxValue;

            char[] minCombination = null;

            foreach (var guess in _guesses)
            {
                int max = 0;

                foreach (var outcome in _outcomes)
                {
                    var count = 0;

                    foreach (var solution in m_combinations)
                    {
                        outcome o = check(guess, solution);
                        //Static function Check to compare the guess to the solution
                        if (o == outcome)
                        {
                            count++;
                        }
                    }
                    if (count > max)
                    {
                        max = count;
                    }
                }
                if (max < min)
                {
                    min = max;
                    minCombination = guess;
                }
            }
            m_prevGuess = minCombination;
            return minCombination;
        }

        /// <summary>
        /// Eliminate guesses from the list of possible guesses.
        /// </summary>
        /// <param name="_outcomes">An array of all possible outcomes</param>
        /// <param name="_guesses">List of possible guesses</param>
        /// <param name="_feedback">Feedback from the previous guess</param>
        /// <param name="_previousGuess">The previously made guess</param>
        /// <returns></returns>
        private List<char[]> elimination(outcome[] _outcomes, List<char[]> _guesses, outcome _feedback, char[] _previousGuess)
        {
            List<char[]> guesses_to_keep = new List<char[]>();

            //Eliminate all guesses that wouldn't get the same outcome as our feedback
            foreach (var guess in _guesses)
            {
                outcome o = check(_previousGuess, guess);
                if ((o == _feedback) && (guess != _previousGuess))
                {
                    guesses_to_keep.Add(guess);

                }
            }
            return guesses_to_keep;
        }
    }
}
