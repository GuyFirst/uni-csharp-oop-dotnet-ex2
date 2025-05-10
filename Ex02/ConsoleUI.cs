using System;

namespace Ex02
{
    public class ConsoleUI      //ROY
    {
        private const int k_Guess = 0;      //remember CONST MEANS #DEFINE IN CSHARP
        private const int k_Result = 1;     //remember CONST MEANS #DEFINE IN CSHARP
        
        public static void PrintBoard(string[,] i_GameHistory, int i_MaxTries)
        {
            ConsoleUtils.Screen.Clear();
            Console.WriteLine("Current board status:");
            Console.WriteLine("|Pins:    |Result:|");
            Console.WriteLine("|=========|=======|");
            Console.WriteLine("| # # # # |       |");
            Console.WriteLine("|=========|=======|");

            for (int turnIndex = 0; turnIndex < i_MaxTries; turnIndex++)
            {
                string historyGuess = i_GameHistory[turnIndex, k_Guess];
                string historyResult = i_GameHistory[turnIndex, k_Result];
                string spacedHistoryGuess = string.Join(" ", historyGuess.ToCharArray());
                string spacedHistoryResult = string.Join(" ", historyResult.ToCharArray());

                Console.WriteLine($"| {spacedHistoryGuess} |{spacedHistoryResult}|");
                Console.WriteLine("|=========|=======|");
            }

        }
    }
}