using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex02
{
    public class ConsoleUI
    {
        private const int k_GuessColumn = 0;
        private const int k_ResultColumn = 1;
        private const int k_ResultPrintPadding = 7;

        public static void PrintBoard(string[,] i_GameHistory, int i_MaxTries, string i_SecretWord = null)
        {
            ConsoleUtils.Screen.Clear();
            Console.WriteLine("Current board status:");
            Console.WriteLine("|Pins:    |Result:|");
            Console.WriteLine("|=========|=======|");

            if (i_SecretWord != null)
            {
                string spacedSecretWord = string.Join(" ", i_SecretWord.ToCharArray());
                Console.WriteLine($"| {spacedSecretWord} |       |");
            }
            else
            {
                Console.WriteLine("| # # # # |       |");
            }

            Console.WriteLine("|=========|=======|");

            for (int turnIndex = 0; turnIndex < i_MaxTries; turnIndex++)
            {
                string historyGuess = i_GameHistory[turnIndex, k_GuessColumn] ?? "";
                string historyResult = i_GameHistory[turnIndex, k_ResultColumn] ?? "";

                if (string.IsNullOrEmpty(historyGuess) && string.IsNullOrEmpty(historyResult))
                {
                    Console.WriteLine("|         |       |");
                }
                else
                {
                    string spacedHistoryGuess = string.Join(" ", historyGuess.ToCharArray());
                    string spacedHistoryResult = string.Join(" ", historyResult.ToCharArray()).PadRight(k_ResultPrintPadding);
                    Console.WriteLine($"| {spacedHistoryGuess} |{spacedHistoryResult}|");
                }

                Console.WriteLine("|=========|=======|");
            }

            Console.WriteLine();
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

        public static string AskFromUserToTakeAGuess(out bool o_UserDecidedToQuit)
        {
            o_UserDecidedToQuit = false;
            InputValidator inputValidator = new InputValidator();
            bool isUserInputValid = false;
            string userInput = null;

            Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");

            while (!isUserInputValid)
            {
                userInput = Console.ReadLine()?.ToUpper();
                isUserInputValid = inputValidator.IsInputValid(userInput, out o_UserDecidedToQuit);

                if (!isUserInputValid && !o_UserDecidedToQuit)
                {
                    Console.WriteLine(inputValidator.ReasonOfBadInput);
                    Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");
                }
            }

            return userInput;
        }

        public static GuessHandler ReadGuessFromUser(out bool o_UserWantsToQuit)
        {
            string userInput = AskFromUserToTakeAGuess(out o_UserWantsToQuit);

            if (o_UserWantsToQuit)
            {
                return default;
            }

            return ConvertStringToGuessHandler(userInput);
        }

        public static void ShowBoard(HistoryOfGuesses i_History, int i_MaxTries, GuessHandler i_SecretWord = default)
        {
            string[,] boardData = ConvertHistoryToStringBoard(i_History, i_MaxTries);
            string secretAsString = i_SecretWord.Equals(default(GuessHandler)) ? null : ConvertGuessHandlerToString(i_SecretWord);
            PrintBoard(boardData, i_MaxTries, secretAsString);
        }

        private static string[,] ConvertHistoryToStringBoard(HistoryOfGuesses i_History, int i_MaxTries)
        {
            string[,] board = new string[i_MaxTries, 2];
            int rowIndex = 0;

            foreach (RowOfGuesses row in i_History.RowOfGuesses)
            {
                board[rowIndex, k_GuessColumn] = ConvertGuessHandlerToString(row.UserGuess);
                board[rowIndex, k_ResultColumn] = ConvertFeedbackToString(row.Feedback);
                rowIndex++;
            }

            return board;
        }


        public static GuessHandler ConvertStringToGuessHandler(string i_Input)
        {
            List<GuessHandler.GuessCollectionOptions> guessList = new List<GuessHandler.GuessCollectionOptions>();

            foreach (char ch in i_Input)
            {
                if (Enum.TryParse(ch.ToString(), out GuessHandler.GuessCollectionOptions parsedChar))
                {
                    guessList.Add(parsedChar);
                }
                
            }

            return new GuessHandler(guessList);
        }

        private static string ConvertGuessHandlerToString(GuessHandler i_Guess)
        {
            return string.Concat(i_Guess.Guess.Select(g => g.ToString()));
        }

        private static string ConvertFeedbackToString(FeedbackOfGuess i_Feedback)
        {
            StringBuilder result = new StringBuilder();

            foreach (var feedback in i_Feedback.feedbackOfGuessTypes)
            {
                if (feedback == FeedbackOfGuess.FeedbackOfGuessType.ExactPlace)
                {
                    result.Append('V');
                }
            }

            foreach (var feedback in i_Feedback.feedbackOfGuessTypes)
            {
                if (feedback == FeedbackOfGuess.FeedbackOfGuessType.WrongPlace)
                {
                    result.Append('X');
                }
            }

            return result.ToString();
        }

        public static bool PrintWinMessage(int i_StartingNumberOfGuess, int i_NumberOfGuessingRemained)
        {
            Console.WriteLine($"Congratulations! You guessed after {i_StartingNumberOfGuess - i_NumberOfGuessingRemained} steps!");
            return askToPlayAgain();
        }

        public static bool PrintLoseMessage()
        {
            Console.WriteLine("No more guesses allowed. You Lost!");
            return askToPlayAgain();
        }

        public static bool PrintQuitMessage()
        {
            Console.WriteLine("Thank you for playing! Come Again! Goodbye!");
            return false;
        }

        private static bool askToPlayAgain()
        {
            const string k_YesIndicator = "Y";
            const string k_NoIndicator = "N";

            Console.WriteLine("Would you like to start a new game? <Y/N>");
            string userInput = Console.ReadLine()?.ToUpper();

            while (userInput != k_YesIndicator && userInput != k_NoIndicator)
            {
                Console.WriteLine("Invalid input. Please enter 'Y' for Yes, or 'N' for No.");
                userInput = Console.ReadLine()?.ToUpper();
            }

            return (userInput == k_YesIndicator);
        }
    }
}
