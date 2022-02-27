using System;
using System.Linq;
using System.Text;

namespace PracticeSolution.CoreLogic.CryptographyAlgorithms
{
    public class Cryptographer
    {
        #region Cezar

        public string CezarEncode(string inputString, byte key)
        {
            var bytes = Encoding.Unicode.GetBytes(inputString).Select(b => b += key).ToArray();
            return Encoding.Unicode.GetString(bytes);
        }
        
        public string CezarDecode(string inputString, byte key)
        {
            var b = Encoding.Unicode.GetBytes(inputString);
            for (var i = 0; i < b.Length; i++)
                b[i] -= key;
            return Encoding.Unicode.GetString(b);
        }
        
        #endregion

        #region Vizner

        public string ViznerEncode(string inputString, string key)
        {
            var bytes = Encoding.Unicode.GetBytes(inputString);
            var keys = Encoding.Unicode.GetBytes(key);
            var currentIndex = 0;
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] += keys[currentIndex++];
                if (currentIndex == keys.Length)
                    currentIndex = 0;
            }
                
            return Encoding.Unicode.GetString(bytes);
        }
        
        public string ViznerDecode(string inputString, string key)
        {
            var bytes = Encoding.Unicode.GetBytes(inputString);
            var keys = Encoding.Unicode.GetBytes(key);
            var currentIndex = 0;
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] -= keys[currentIndex++];
                if (currentIndex == keys.Length)
                    currentIndex = 0;
            }
            return Encoding.Unicode.GetString(bytes);
        }
        
        #endregion
    }
}