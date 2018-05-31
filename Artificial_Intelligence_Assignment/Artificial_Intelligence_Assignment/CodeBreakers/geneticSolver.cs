using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Artificial_Intelligence_Assignment.Mastermind;

namespace Artificial_Intelligence_Assignment.CodeBreakers
{
    class geneticSolver : ai_Solver
    {
        Random rnd = new Random();

        private int maximumGen = 20;

        private int crossoverChance = 1; //Currently unused as crossover isn't used

        private int crossoverMutationChance = 2;

        private int permutationChance = 3;

        private int inversionChance = 4;

        private int bWeight = 5;

        private int wWeight = 3;

        char[] prevGuess = new char[4]; //The previous guess we made

        Dictionary<char[], int> guessFitness = new Dictionary<char[], int>(); //New Guesses with their fitness scores

        public geneticSolver()
        {
            generateCombinations();
        }

        /// <summary>
        /// Method to generate a guess and return it
        /// </summary>
        /// <param name="_feedback"> Previous guess feedback </param>
        /// <param name="_initialGuess"> Boolean to define whether it is the first guess </param>
        /// <returns></returns>
        public char[] guess(outcome _feedback, bool _initialGuess)
        {
            if (_initialGuess == false)
            {
                char[] initialguess = new char[4];
                initialguess[0] = '1';
                initialguess[1] = '1';
                initialguess[2] = '2';
                initialguess[3] = '2';

                prevGuess = initialguess;

                return initialguess;
            }
            else
            {
                //Generate the population
                geneticEvolution(prevGuess);

                //Compare our population to the previous guess and it's feedback
                comparison(_feedback, prevGuess);

                var sortedDictionary = guessFitness.OrderByDescending(i => i.Value);

                //Take the highest scoring guess and play it
                char[] guess = sortedDictionary.First().Key;

                //Set it as previousGuess
                prevGuess = guess;

                return guess;
            }
        }

        /// <summary>
        /// Comparing guess population to previous guess and feedback
        /// </summary>
        /// <param name="_feedback">Previous guess feedback</param>
        /// <param name="_prevGuess">the previous guess array</param>
        private void comparison(outcome _feedback, char[] _prevGuess)
        {
            //Foreach Guess in guesses, get a score from fitness_score
            guessFitness.Clear();

            foreach (char[] _guess in m_guesses)
            {
                guessFitness.Add(_guess, fitnessScore(_guess, _feedback, prevGuess));
            }
        }

        /// <summary>
        /// give all the guesses a fitness score, based on the comparison to previous feedback
        /// </summary>
        /// <param name="_guess"> The guess we want to compare and score</param>
        /// <param name="_feedback"> The previous feedback </param>
        /// <param name="_prevGuess"> The previous guess </param>
        /// <returns></returns>
        private int fitnessScore(char[] _guess, outcome _feedback, char[] _prevGuess)
        {
            //Takes a guess and compares it to a feedback code
            //Returns a score based on the probability of "_guess" being a correct guess

            outcome o = check(_guess, prevGuess);

            int score = 0;

            score += o.m_black - _feedback.m_black * bWeight;

            score += o.m_white - _feedback.m_white * wWeight;

            return score;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_prevGuess"></param>
        private void geneticEvolution(char[] _prevGuess)
        {
            //Use crossover, Mutation and Permutation to create a new population of guesses

            //Create a new guess, use the the modifiers to change it then add it to the list of guesses
            int Seed = (int)DateTime.Now.Ticks;

            Random _rnd = new Random(Seed);
            m_guesses.Clear();

                for (int y = 0; y <= maximumGen; y++)
                {
                    char[] newGuess = _prevGuess;
                    
                    //Temporarily removed crossover as the logistics of finding two guesses to crossover on the first iteration is causing issues

                    /*if (crossover_chance > rnd.Next(0, 100))
                    {
                        newGuess = crossover(guesses[2], prevGuess);
                    }
                    */

                    //Not true "chance", picks one of these 3 possibilities to reduce time taken to generate the population

                    int x = _rnd.Next(0, 4);

                    if (permutationChance == x)
                    {
                        newGuess = permute(_prevGuess);
                    }
                    else if (inversionChance == x)
                    {
                        newGuess = invert(_prevGuess);
                    }
                    else if (crossoverMutationChance == x)
                    {
                        newGuess = mutate(_prevGuess);
                    }

                    if (!m_guesses.Contains(newGuess))
                    {
                        m_guesses.Add(newGuess);
                    }
                }
            }

        /// <summary>
        /// Currently Unused function, would perform crossover
        /// </summary>
        /// <param name="code"> The guess we want to crossover with </param>
        /// <param name="prevGuess"> The previous guess </param>
        /// <returns></returns>
        private char[] crossover(char[] code, char[] prevGuess) 
        {
            //Crossover two codes values randomly
            char[] newCode = new char[4];

            for (int i = 0; i < 5; i++)
            {
                int x = rnd.Next(0, 4);
                int y = rnd.Next(0, 4);
                newCode[x] = prevGuess[y];
            }

            return newCode;
        }

        /// <summary>
        /// Mutation function for genetic algorithm
        /// </summary>
        /// <param name="prevGuess"> A char array containing the previous guess </param>
        /// <returns></returns>
        private char[] mutate(char[] prevGuess)
        {
            //Randomly change 1 value in the guess

            string chars = "123456";

            int i = rnd.Next(0, 4);
            int num = rnd.Next(0, chars.Length - 1);
            prevGuess[i] = chars[num];

            return prevGuess;
        }

        /// <summary>
        /// Permuation function
        /// </summary>
        /// <param name="prevGuess"> A char array containing the previous guess </param>
        /// <returns></returns>
        private char[] permute(char[] prevGuess)
        {
            //Get two positions and swap them

            int x = rnd.Next(0, 4);
            int y = rnd.Next(0, 4);

            char saveX = prevGuess[x];

            prevGuess[x] = prevGuess[y];

            prevGuess[y] = saveX;

            return prevGuess;
        }

        /// <summary>
        /// Invert function
        /// </summary>
        /// <param name="code"> A char array containing the previous guess </param>
        /// <returns></returns>
        private char[] invert(char[] code)
        {
            //Reverse the order of values in the guess

            char[] tempcode = code;
            code[0] = tempcode[3];
            code[1] = tempcode[2];
            code[2] = tempcode[1];
            code[3] = tempcode[0];

            return code;
        }    
    }
}

