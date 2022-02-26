using System;

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
    }
}