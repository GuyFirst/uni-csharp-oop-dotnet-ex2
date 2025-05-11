using System;

namespace Ex02
{
    public class GameData
    {
        public const int k_Guess = 0;           //remember CONST MEANS #DEFINE IN CSHARP
        public const int k_Result = 1;          //remember CONST MEANS #DEFINE IN CSHARP
        private const int k_ColumnSize = 2;     //remember CONST MEANS #DEFINE IN CSHARP
        public readonly int r_MaxUserGuesses;
        public int m_RemaningNumberOfGuesses { get; set; }
        public string SecretWord { get; set; }
        public string[,] GuessesAndResultsHistory { get; set; } 

        public GameData(string i_SecretWord, int i_MaxUserGuesses) 
        {
            SecretWord = i_SecretWord;
            r_MaxUserGuesses = i_MaxUserGuesses;
            m_RemaningNumberOfGuesses = i_MaxUserGuesses;
            GuessesAndResultsHistory = new string[r_MaxUserGuesses, k_ColumnSize];
        }

        public void AddGuessAndFeedback(string i_Guess, string i_Feedback)
        {
            GuessesAndResultsHistory[m_RemaningNumberOfGuesses, k_Guess] = i_Guess;
            GuessesAndResultsHistory[m_RemaningNumberOfGuesses, k_Result] = i_Feedback;
        }
    }
}