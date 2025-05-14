using System.Collections.Generic;

namespace Ex02
{
    public struct GuessHandler
    {
        public enum eGuessCollectionOptions { A, B, C, D, E, F, G, H }
        public List<eGuessCollectionOptions> Guess { get; set; }

        public GuessHandler(List<eGuessCollectionOptions> i_Guess) 
        {
            Guess = i_Guess;
        }
    }
}