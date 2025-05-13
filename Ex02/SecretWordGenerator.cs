using System;
using System.Collections.Generic;

namespace Ex02
{
    public class SecretWordGenerator
    {
        private List<char> m_allowedLetters;
        private Random m_random;

        public SecretWordGenerator(List<char> allowedLetters, Random random)
        {
            this.m_allowedLetters = new List<char>(allowedLetters);
            this.m_random = random;
        }

        public char[] Generate(int wordLength)
        {
            char[] secret = new char[wordLength];
            List<char> pool = new List<char>(m_allowedLetters);

            for(int i = 0; i < wordLength; i++)
            {
                int index = m_random.Next(0, pool.Count);
                secret[i] = pool[index];
                pool.RemoveAt(index);
            }

            return secret;
        }
    }
}