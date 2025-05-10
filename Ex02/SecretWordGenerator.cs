using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02
{
    public class SecretWordGenerator //ROY
    {
        private readonly Random r_RandomGenerator = new Random();
        private readonly char[] r_CharsBank = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};
        public const int k_SecretWordLength = 4;     //remember CONST MEANS #DEFINE IN CSHARP

        public string GenerateSecretWord()
        {
            StringBuilder secretWord = new StringBuilder();
            List<char> availableChars = new List<char>(r_CharsBank);

            for (int i = 0; i < k_SecretWordLength; i++)
            {
                int randomIndex = r_RandomGenerator.Next(availableChars.Count);
                secretWord.Append(availableChars[randomIndex]);
                availableChars.RemoveAt(randomIndex);
            }

            return secretWord.ToString();
        }
    }
}