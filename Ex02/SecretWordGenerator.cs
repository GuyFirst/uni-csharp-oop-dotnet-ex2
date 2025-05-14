using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02
{
    public class SecretWordGenerator
    {
        private static readonly Random sr_RandomGenerator = new Random();
        private static readonly char[] sr_CharsBank = getCharsBankFromEnum();
        public const int k_SecretWordLength = 4;

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

        private static char[] getCharsBankFromEnum()
        {
            Array enumValues = Enum.GetValues(typeof(GuessHandler.eGuessCollectionOptions));
            char[] chars = new char[enumValues.Length];
            for (int i = 0; i < enumValues.Length; i++)
            {
                chars[i] = enumValues.GetValue(i).ToString()[0];
            }
            return chars;
        }
    }
}