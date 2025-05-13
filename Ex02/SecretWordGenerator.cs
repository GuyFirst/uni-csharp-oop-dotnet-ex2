using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02
{
    public class SecretWordGenerator //ROY
    {
        private static readonly Random sr_RandomGenerator = new Random();
        private static readonly char[] sr_CharsBank = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};
        public const int k_SecretWordLength = 4;     //remember CONST MEANS #DEFINE IN CSHARP

        public static string GenerateSecretWord()
        {
            StringBuilder secretWord = new StringBuilder();
            List<char> availableChars = new List<char>(sr_CharsBank);

            for (int i = 0; i < k_SecretWordLength; i++)
            {
                int randomIndex = sr_RandomGenerator.Next(availableChars.Count);
 
                secretWord.Append(availableChars[randomIndex]);
                availableChars.RemoveAt(randomIndex);
            }

            return secretWord.ToString();
        }
    }
}