using System;

namespace Ex02
{
    public class GameData
    {
        public string SecretWord { get; set; }
        public readonly int r_MaxUserGuesses;
        private const int k_ColumnSize = 2;
        public string[,] GuessesAndResultsHistory { get; set; } 

        public GameData(string i_SecretWord, int i_MaxUserGuesses) 
        {
            SecretWord = i_SecretWord;
            r_MaxUserGuesses = i_MaxUserGuesses;
            GuessesAndResultsHistory = new string[r_MaxUserGuesses, k_ColumnSize];
        }

        public string addGuessAndFeedback(string i_Guess, string i_Feedback)
        {
            
        }


        
    }
}
