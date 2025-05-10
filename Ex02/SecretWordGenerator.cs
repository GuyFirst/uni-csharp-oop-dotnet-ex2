using System;
using System.Collections.Generic;

namespace Ex02
{
    public class SecretWordGenerator //ROY
    {
        private readonly Random r_RandomGenerator = new Random();
        private readonly char[] r_CharsBank = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};
        private const int k_SecretWordLength = 4;     //remember CONST MEANS #DEFINE IN CSHARP

        public string GenerateSecretWord()
        {
            string secretWord = string.Empty;
            List<char> availableChars = new List<char>(r_CharsBank);

            for (int i = 0; i < k_SecretWordLength; i++)
            {
                int randomIndex = r_RandomGenerator.Next(availableChars.Count);
                secretWord += availableChars[randomIndex];
                availableChars.RemoveAt(randomIndex);
            }

            return secretWord;
        }
        
    }
}