using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
            Console.SetCursorPosition(0, 0);
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

            ConsoleUtils.Screen.Clear();
            while (!validGuess)
            {
                Console.Write("Please provide your number of guesses between 4 and 10: ");
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

        public static string AskFromUserToTakeAGuess(out bool io_UserDecidedToQuit)
        {
            io_UserDecidedToQuit = false;
            InputValidator inputValidator = new InputValidator();
            bool isUserInputValid = false;
            string userInput = string.Empty;

            Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");

            while (!isUserInputValid)
            {
                userInput = Console.ReadLine();     // CASE-SENSITIVE 
                isUserInputValid = inputValidator.IsInputValid(userInput, out io_UserDecidedToQuit);

                if (!isUserInputValid && !io_UserDecidedToQuit)
                {
                    Console.WriteLine(inputValidator.ReasonOfBadInput);
                    Console.WriteLine("Please type your next guess <A B C D> or 'Q' to quit");
                }
            }

            return userInput;
        }

        public static GuessHandler ConvertStringToGuessHandler(string i_Input)
        {
            List<GuessHandler.eGuessCollectionOptions> guessList = new List<GuessHandler.eGuessCollectionOptions>();

            foreach (char ch in i_Input)
            {
                if (Enum.TryParse(ch.ToString(), ignoreCase: false, out GuessHandler.eGuessCollectionOptions parsedChar))
                {
                    guessList.Add(parsedChar);
                }
            }

            return new GuessHandler(guessList);
        }

        public static GuessHandler ReadGuessFromUser(out bool io_UserWantsToQuit)
        {
            string userInput = AskFromUserToTakeAGuess(out io_UserWantsToQuit);
            GuessHandler userGuess = io_UserWantsToQuit ? default : ConvertStringToGuessHandler(userInput);

            return userGuess;
        }


        public static void ShowBoard(HistoryOfGuesses i_History, int i_MaxTries, GuessHandler i_SecretWord = default)
        {
            string[,] boardData = convertHistoryToStringBoard(i_History, i_MaxTries);
            string secretAsString = i_SecretWord.Equals(default(GuessHandler)) ? null : convertGuessHandlerToString(i_SecretWord);
            
            PrintBoard(boardData, i_MaxTries, secretAsString);
        }

        private static string[,] convertHistoryToStringBoard(HistoryOfGuesses i_History, int i_MaxTries)
        {
            int rowIndex = 0;
            const int k_NumberOfColumns = 2;
            string[,] board = new string[i_MaxTries, k_NumberOfColumns];

            foreach (RowOfGuesses row in i_History.RowOfGuesses)
            {
                board[rowIndex, k_GuessColumn] = convertGuessHandlerToString(row.UserGuess);
                board[rowIndex, k_ResultColumn] = convertFeedbackToString(row.Feedback);
                rowIndex++;
            }

            return board;
        }

        private static string convertGuessHandlerToString(GuessHandler i_Guess)
        {
            StringBuilder result = new StringBuilder();

            foreach (GuessHandler.eGuessCollectionOptions letter in i_Guess.Guess)
            {
                result.Append(letter.ToString());
            }

            return result.ToString();
        }

        private static string convertFeedbackToString(FeedbackOfGuess i_Feedback)
        {
            StringBuilder result = new StringBuilder();
            int exactPlaceCount = 0;
            int wrongPlaceCount = 0;

            foreach (FeedbackOfGuess.eFeedbackOfGuessType feedback in i_Feedback.m_FeedbackOfGuessTypes)
            {
                switch(feedback)
                {
                    case FeedbackOfGuess.eFeedbackOfGuessType.ExactPlace:
                        exactPlaceCount++;
                        break;
                    case FeedbackOfGuess.eFeedbackOfGuessType.WrongPlace:
                        wrongPlaceCount++;
                        break;
                }
            }

            result.Append('V', exactPlaceCount);
            result.Append('X', wrongPlaceCount);

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