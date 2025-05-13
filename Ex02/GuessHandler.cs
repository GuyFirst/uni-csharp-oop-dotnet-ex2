using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public struct GuessHandler
    {
        public enum GuessCollectionOptions { A, B, C, D, E, F, G, H }

        public List<GuessCollectionOptions> Guess { get; set; }

        public GuessHandler(List<GuessCollectionOptions> i_Guess) 
        {
            Guess = i_Guess;
        }
       
    }
}
