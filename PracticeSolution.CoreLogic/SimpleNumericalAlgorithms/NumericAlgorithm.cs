using System;

namespace PracticeSolution.CoreLogic.SimpleNumericalAlgorithms
{
    public class NumericAlgorithm
    {
        /// <summary>
        /// Calculates the factorial of a number
        /// </summary>
        /// <param name="number">Calculated number</param>
        /// <returns>Fuctorial</returns>
        public int Factorial(int number)
        {
            if (number <= 1)
            {
                return 1;
            }
            return number * Factorial(--number);
        }

        #region Fibonacci

        /// <summary>
        /// Calculates a number from the Fibonacci sequence
        /// </summary>
        /// <param name="number">Sequence number</param>
        /// <returns>Fibonacci sequence number "number"</returns>
        public int Fibonacci(int number) => Fibonacci(0, 1, 2, number);
        private int Fibonacci(int first, int second, int current, int number)
        {
            if (current >= number)
            {
                return second;
            }
            return Fibonacci(second, first + second, ++current, number);
        }

        #endregion
        
        /// <summary>
        /// Greatest common divisor
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second Number</param>
        /// <returns>Greatest common divisor</returns>
        public int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }
            return a + b;
        }

        /// <summary>
        /// Least common multiple
        /// </summary>
        /// <param name="a">First number</param>
        /// <param name="b">Second number</param>
        /// <returns>Least common multiple</returns>
        public int LCM(int a, int b)
        {
            return a / GCD(a, b) * b;
        }
        
        public static bool IsSimple(int number)
        {
            double sqrt = Math.Sqrt(number);
            for(var i = 2; i <= sqrt; i++)
                if (number % i == 0)
                    return false;
            return true;
        }
    }
}