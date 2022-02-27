using System;
using PracticeSolution.CoreLogic.CryptographyAlgorithms;

namespace PracticeSolution.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Cryptographer cript = new Cryptographer();

            string s = "Hello World";
            Console.WriteLine(s);
            
            var s1 = cript.CezarEncode(s, 3);
            Console.WriteLine(s1);
            var s2 = cript.CezarDecode(s1, 3);
            Console.WriteLine(s2);

            var s3 = cript.ViznerEncode(s2, "keykey");
            Console.WriteLine(s3);
            
            var s4 = cript.ViznerDecode(s3, "keykey");
            Console.WriteLine(s4);
        }
    }
}