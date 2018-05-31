using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artificial_Intelligence_Assignment.CodeBreakers
{
    class player
    {
        private char[] codeGuess = new char[4];

        /// <summary>
        /// Allow the user to input a guess and return it
        /// </summary>
        /// <returns></returns>
        public char[] guess()
        {
            bool isValid = false;

            while (isValid == false)
            {
                Console.WriteLine("Enter a guess");

                string playerGuess = Console.ReadLine();

                if ((validFormat(playerGuess) == true) && (playerGuess.Count() < 5))
                {
                    codeGuess = playerGuess.ToCharArray();
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Please enter a valid code");
                }
            }

            return codeGuess;
        }

        /// <summary>
        /// Simple input validation
        /// </summary>
        /// <param name="format"> The string to be validated </param>
        /// <returns></returns>
        bool validFormat(string format)
        {
            string allowable = "123456";

            foreach (char c in format)
            {
                if (!allowable.Contains(c.ToString()))
                    return false;
            }
            return true;
        }
    }
}
