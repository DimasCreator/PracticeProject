using System;
using System.Threading.Channels;
using PracticeSolution.CoreLogic.CryptographyAlgorithms;

namespace PracticeSolution.Cmd
{
    class Program
    {
        private const int B = 16;
        static void Main(string[] args)
        {
            dtest();
            // ftest("TESTtestTeSt");
            // ftest("Программа рама");
            // ftest("mdkacvnra32412");
            // ftest("vdamvkfmaskv32423423");
            // ftest("affjir231");
        }

        static void dtest()
        {
            DES d = new DES();
            var enc = d.buttonEncrypt_Click("TESTtestTeSt", "key");
            Console.WriteLine(enc);
            d.buttonDecipher_Click(enc, "key");
        }
        
        static void ftest(string text)
        {
            var enc = Feistel.Encrypt(text, 6, "key", 5);
            var dec = Feistel.Decrypt(enc, 6, "key", 5);
            Console.WriteLine(text + " - было");
            Console.WriteLine(enc + " - зашифрованное");
            Console.WriteLine(dec + " - расшифрованное");
            Console.WriteLine("------------");
        }

        static void f44(string str)
        {
            var lenght = str.Length;
            var offset = (int) Math.Ceiling(lenght / (double) B) * B - lenght;
            str = string.Concat(str, new string('0', offset));
            Console.WriteLine(str);
            Console.WriteLine(str.Length);
        }
    }
}