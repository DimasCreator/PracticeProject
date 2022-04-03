using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PracticeSolution.CoreLogic.CryptographyAlgorithms
{
    public static class Feistel
    {
        public static string Encrypt(string text, int blockSize, string key, int round) =>
            new string(Encrypt(text.ToCharArray(), blockSize, key, round));
        
        public static string Decrypt(string text, int blockSize, string key, int round) =>
            new string(Decrypt(text.ToCharArray(), blockSize, key, round));
        
        public static char[] Encrypt(char[] text, int blockSize, string key, int round)
        {
            var blocks = GetTextBlocks(text, blockSize).ToArray();
            
            for (var i = 0; i < round; i++)
            for (var j = 0; j < blocks.Length; j++)
                    blocks[j] = CryptBlock(blocks[j], GetNewKey(key, i), blockSize);
            
            var result = new char[blocks.Length * blockSize];
            var iter = 0;
            foreach (var block in blocks)
                for (var j = 0; j < blockSize; j++)
                    result[iter++] = block[j];
            return result;
        }
        
        public static char[] Decrypt(char[] text, int blockSize, string key, int round)
        {
            var blocks = GetTextBlocks(text, blockSize).ToArray();
            
            for (var i = round - 1; i >= 0; i--)
            {
                for (var j = 0; j < blocks.Length; j++)
                {
                    bool p = false, r = false;
                    if (i == round - 1) p = true;
                    if (i == 0) r = true;
                    blocks[j] = CryptBlock(blocks[j],GetNewKey(key, i), blockSize,p, r);
                }
            }
            
            var result = new char[blocks.Length * blockSize];
            var iter = 0;

            foreach (var block in blocks)
                for (var j = 0; j < blockSize; j++)
                    result[iter++] = block[j];
            return result;
        }

        private static IEnumerable<char[]> GetTextBlocks(char[] text, int blockSize)
        {
            var lenght = text.Length;
            if (lenght % blockSize != 0)
            {
                var offset = (int) Math.Ceiling(lenght / (double) blockSize) * blockSize - lenght;
                text = string.Concat(new string(text), new string(' ', offset)).ToCharArray();
            }
            var result = new string[text.Length / blockSize];
            for (var i = 0; i < result.Length; i++)
                yield return text.Skip(i * blockSize).Take(blockSize).ToArray();
        }
        
        private static string GetNewKey(string key, int round)
        {
            var result = new StringBuilder();
            key.Aggregate(result, (builder, elem) => result.Append((char)(elem * (int) Math.Pow(2, round))));
            return result.ToString();
        }
        
        private static char[] CryptBlock(char[] block, string key, int blockSize, bool Reverse = false, bool notReverse = false)
        {
            var blockL = block.Take(blockSize / 2).ToArray();
            var blockR = block.Skip(blockSize / 2).ToArray();
            
            if (Reverse)
                (blockL, blockR) = (blockR, blockL);
            
            var temp = new char[blockSize / 2];
            var iter = 0;
            for (var i = 0; i < blockL.Length; i++)
            {
                temp[i] = (char) (blockR[i] ^ blockL[i] ^ key[iter++]);
                if (iter >= key.Length)
                    iter = 0;
            }
            
            return notReverse ? blockL.Concat(temp).ToArray() : temp.Concat(blockL).ToArray();
        }
    }
}