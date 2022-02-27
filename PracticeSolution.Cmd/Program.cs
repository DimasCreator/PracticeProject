using System;
using System.Linq;
using PracticeSolution.CoreLogic.CryptographyAlgorithms;
using PracticeSolution.CoreLogic.PracticeTasks;

namespace PracticeSolution.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            PracticeTasks_p1 p = new PracticeTasks_p1();
            foreach (var v in p.Task5(100))
            {
                Console.WriteLine(v);
            }
        }
    }
}