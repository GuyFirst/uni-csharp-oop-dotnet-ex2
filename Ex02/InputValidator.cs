using System.Collections.Generic;
using System;

namespace Ex02
{
    public class InputValidator
    {
        private int m_wordLength;
        private List<char> m_allowedSymbols;

        public InputValidator(int wordLength, List<char> allowedSymbols)
        {
            this.m_wordLength = wordLength;
            this.m_allowedSymbols = new List<char>(allowedSymbols);
        }

        public bool Validate(string input)
        {
            // 1) Length check
            if (input.Length != m_wordLength)
            {
                Console.WriteLine(
                    "Please enter a " + m_wordLength + "-letter guess.");
                return false;
            }

            // 2) Symbol‐range check
            string upper = input.ToUpper();
            for (int i = 0; i < upper.Length; i++)
            {
                char c = upper[i];
                // if c not in the allowed list
                if (m_allowedSymbols.IndexOf(c) < 0)
                {
                    Console.WriteLine(
                        "Invalid letter '" + c +
                        "'.  Use only: " +
                        string.Join(" ", m_allowedSymbols));
                    return false;
                }
            }

            return true;
        }
    }
}