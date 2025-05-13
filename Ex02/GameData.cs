using System.Collections.Generic;

namespace Ex02
{
    public class GameData
    {
        public const int k_Guess = 0;           //remember CONST MEANS #DEFINE IN CSHARP
        public const int k_Result = 1;          //remember CONST MEANS #DEFINE IN CSHARP
        public const int k_ColumnSize = 2;     //remember CONST MEANS #DEFINE IN CSHARP
        public const int k_WordLength = 4;
        //public List<char> m_Allowed = new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };


        //public readonly int r_MaxUserGuesses;
        //public int RemainingNumberOfGuesses { get; set; }
        //public string SecretWord { get; set; }
        //public string[,] GuessesAndResultsHistory { get; set; }

        //public GameData(string i_SecretWord, int i_MaxUserGuesses) 
        //{
        //    SecretWord = i_SecretWord;
        //    r_MaxUserGuesses = i_MaxUserGuesses;
        //    RemainingNumberOfGuesses = i_MaxUserGuesses;
        //    GuessesAndResultsHistory = new string[r_MaxUserGuesses, k_ColumnSize];
        //}

        //public void AddGuessAndFeedback(string i_Guess, string i_Feedback)
        //{
        //    int currentGuessIndex = r_MaxUserGuesses - RemainingNumberOfGuesses;

        //    GuessesAndResultsHistory[currentGuessIndex, k_Guess] = i_Guess;
        //    GuessesAndResultsHistory[currentGuessIndex, k_Result] = i_Feedback;
        //}
    }
}