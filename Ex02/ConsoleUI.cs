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
                string historyGuess = i_GameHistory[turnIndex, k_Guess] ?? "";
                string historyResult = i_GameHistory[turnIndex, k_Result] ?? "";

                if (string.IsNullOrEmpty(historyGuess) && string.IsNullOrEmpty(historyResult))
                {
                    Console.WriteLine("|         |       |");
                }
                else
                {
                    string spacedHistoryGuess = string.Join(" ", historyGuess.ToCharArray());
                    string spacedHistoryResult = string.Join(" ", historyResult.ToCharArray());
                    Console.WriteLine($"| {spacedHistoryGuess} |{spacedHistoryResult}|");
                }
                Console.WriteLine("|=========|=======|");
            }

        }

        public static int AskUserToEnterNumberOfGuesses()
        {
            bool validGuess = false;
            int numberOfGuesses = 0;

            while (!validGuess)
            {
                Console.WriteLine("Please provide your number of guesses between 4 and 10: ");
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out numberOfGuesses) && numberOfGuesses >= 4 && numberOfGuesses <= 10)
                {
                    validGuess = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 4 and 10.");
                }
            }

            return numberOfGuesses;
        }

        public static string AskFromUserToTakeAGuess(out bool o_GameOverFlag)
        {
            o_GameOverFlag = false;
            InputValidator inputValidator = new InputValidator();
            bool isUserInputValid = false;
            string userInput = null;

            Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");
            while (!isUserInputValid)
            {
                userInput = Console.ReadLine();
                userInput = userInput.ToUpper();
                isUserInputValid = inputValidator.IsInputValid(userInput, out o_GameOverFlag);
                if (!isUserInputValid && !o_GameOverFlag)
                {
                    Console.WriteLine(inputValidator.ReasonOfBadInput);
                    Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");
                    continue;
                }
                isUserInputValid = true;
            }

            return userInput;
        }
    }
}