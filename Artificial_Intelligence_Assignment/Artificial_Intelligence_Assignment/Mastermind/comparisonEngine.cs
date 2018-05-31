using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artificial_Intelligence_Assignment.Display;

namespace Artificial_Intelligence_Assignment.Mastermind
{
    class comparisonEngine
    {
        /// <summary>
        /// Compare the guess made to the winning code
        /// </summary>
        /// <param name="_guess">the guess made</param>
        /// <param name="_code">the "winning" code</param>
        /// <param name="_win">the boolean to define a win</param>
        /// <param name="_tries">The remaining number of tries</param>
        /// <param name="_d">the display class</param>
        /// <returns></returns>
        public char[] compare(char[] _guess, char[] _code, out bool _win, int _tries, display _d)
        {

            //Get feedback and write it out to the screen
            char[] _feedback = feedback(_guess, _code);
            for (int i = 6; i < 10;)
            {
                foreach (char c in _feedback)
                {
                    _d._grid[_tries + 1, i] = c;
                    i++;
                }
            }

            //Write out the guess to the screen so we can see previous guesses
            for (int i = 1; i < 5;)
            {
                foreach (char c in _guess)
                {
                    _d._grid[_tries + 1, i] = c;
                    i++;
                }
            }

            // If the guess matches the secret or "winning" code the game is won
            if (_guess.SequenceEqual(_code))
            {
                _win = true;
            }
            //Otherwise we carry on
            else
            {
                _win = false;
            }

            return feedback(_guess, _code);
        }

        /// <summary>
        /// Compare two character arrays, the guess and the winning code
        /// </summary>
        /// <param name="_guess">The guess that has been made</param>
        /// <param name="_code">The winning code</param>
        /// <returns></returns>
        private char[] feedback(char[] _guess, char[] _code)
        {

            //Get out the values from the guess and code, make temporary arrays to modify for comparison
            char[] tempCode = new char[4];

            char[] tempGuess = new char[4];

            for (int i = 0; i < 4; i++)
            {
                tempCode[i] = _code[i];
            }

            for (int i = 0; i < 4; i++)
            {
                tempGuess[i] = _guess[i];
            }

            //Feedback array to display to the user
            char[] feedback = new char[4];

            //Number of white pins
            int white = 0;

            //Number of black pins
            int black = 0;

            //Iterate through the code and guess arrays to find matching pairs
            for (int i = 0; i < 4; i++)
            {
                if (tempCode[i] == tempGuess[i])
                {
                    //change the values so they won't match anymore
                    tempCode[i] = '-';
                    tempGuess[i] = '.';

                    //Add one to the number of black pins
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
                        tempCode[i] = ',';
                        //Add one to the number of white pins
                        white++;
                        break;
                    }
                }
            }
            feedback = generate(black, white);

            return feedback;
        }

        /// <summary>
        /// Generate white and black pins for drawing to the screen
        /// </summary>
        /// <param name="black">Number of black pins in the feedback</param>
        /// <param name="white">Number of white pins in the feedback</param>
        /// <returns></returns>
        private char[] generate(int black, int white)
        {
            char[] _feedback = new char[4];
            int f = 0;

            //Add the white pins to the feedback
            while (black > 0)
            {
                _feedback[f] = 'B';
                f++;
                black--;
            }

            //Add the black pins to the feedback
            while (white > 0)
            {
                _feedback[f] = 'W';
                f++;
                white--;
            }

            //Add 0s for the empty pins
            while (f < 4)
            {
                _feedback[f] = '0';
                f++;
            }

            return _feedback;
        }
    }
}
