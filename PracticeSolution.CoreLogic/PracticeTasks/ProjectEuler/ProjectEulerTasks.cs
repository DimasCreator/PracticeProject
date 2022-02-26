using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticeSolution.CoreLogic.PracticeTasks.ProjectEuler
{
    public class ProjectEulerTasks
    {
        /// <summary>
        /// Если мы перечислим все натуральные числа ниже 10, которые кратны 3 или 5, мы получим 3, 5, 6 и 9. Сумма этих кратных 23.
        /// Найдите сумму всех кратных 3 или 5 чисел, которые меньше 1000.
        /// </summary>
        public int Task_1()
        {
            var sum = 0;
            for (var i = 1; i < 1000; i++)
                if (i % 3 == 0 || i % 5 == 0)
                    sum += i;
            return sum;
        }
        
        /// <summary>
        /// Каждый новый член в последовательности Фибоначчи генерируется путем добавления двух предыдущих членов.
        /// Начиная с 1 и 2, первые 10 терминов будут: 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, ...
        /// Рассматривая термины в последовательности Фибоначчи, значения которых не превышают четырех миллионов, найдите сумму четных членов.
        /// </summary>
        public int Task_2()
        {
            return GetFibonacciSequence(1, 2, 4000000).Where(x => x % 2 == 0).Sum();
        }
        private IEnumerable<int> GetFibonacciSequence(int first, int second, int max)
        {
            yield return first;
            yield return second;
            int next = first + second;
            while (next < max)
            {
                yield return next;
                first = second;
                int tmp = second;
                second = next;
                next += tmp;
            }
        }
        
        //TODO: Сделать
        /// <summary>
        /// Простыми множителями 13195 являются 5, 7, 13 и 29.
        /// Какой самый большой простой множитель числа 600851475143?
        /// </summary>
        public void Task_3()
        {
            var l = 600851475143;
            var div = (int) Math.Sqrt(600851475143);
            while (div > 1)
            {
                if (l % div == 0 && IsPrime(div))
                {
                    Console.WriteLine(div);
                    return;
                }
                div--;
            }
        }
        private bool IsPrime(int num)
        {
            double sqrt = Math.Sqrt(num);
            for (int i = 2; i <= sqrt; i++)
            {
                if (num % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        
    }
}