using System;
using Ex02.ConsoleUtils;

namespace Ex02
{
    public class ConsoleUI
    {
        public void ShowGuessResult<TSymbol>(
            GuessAttempt<TSymbol> result,
            System.Func<TSymbol[], string> toStringFunc)
        {
            string guessString = toStringFunc(result.Guess);
            string feedbackString = BuildFeedbackString(result.Feedback);
            Console.WriteLine("|" + guessString + "|" + feedbackString + "|");
        }

        private string BuildFeedbackString(LetterFeedback[] i_Feedback)
        {
            char[] symbols = new char[i_Feedback.Length];
            for (int i = 0; i < i_Feedback.Length; i++)
            {
                if (i_Feedback[i] == LetterFeedback.CorrectSpot)
                {
                    symbols[i] = 'V';
                }
                else if (i_Feedback[i] == LetterFeedback.RightLetterWrongSpot)
                {
                    symbols[i] = 'X';
                }
                else
                {
                    symbols[i] = ' ';
                }
            }

            return string.Join(" ", symbols);
        }

        public void ShowGameEnd<TSymbol>(
            GameState state,
            System.Func<TSymbol[], string> toStringFunc,
            TSymbol[] secret)
        {
            if (state == GameState.Won)
            {
                Console.WriteLine("Congratulations! You guessed it!");
            }
            else if (state == GameState.Lost)
            {
                Console.WriteLine("Game over. The secret was: " + toStringFunc(secret));
            }
        }

        public static void PrintBoard(
            string[,] i_GameHistory,
            int i_MaxTries,
            string i_SecretWord = null)
        {
            // 1) clear the screen via the DLL:
            Screen.Clear();

            const int k_ResultPrintPadding = 7;
            Console.WriteLine("Current board status:");
            Console.WriteLine("|Pins:    |Result:|");
            Console.WriteLine("|=========|=======|");

            // 2) Top row = either the masked secret or ####
            if (i_SecretWord != null)
            {
                string spacedSecret = string.Join(" ",
                    i_SecretWord.ToCharArray());
                Console.WriteLine("| " + spacedSecret + " |       |");
            }
            else
            {
                Console.WriteLine("| # # # # |       |");
            }

            Console.WriteLine("|=========|=======|");

            // 3) History rows (guesses + feedback) INSIDE the board
            for (int turn = 0; turn < i_MaxTries; turn++)
            {
                string guess = i_GameHistory[turn, GameData.k_Guess] ?? "";
                string result = i_GameHistory[turn, GameData.k_Result] ?? "";

                if (guess == "" && result == "")
                {
                    Console.WriteLine("|         |       |");
                }
                else
                {
                    // insert spaces between chars
                    string spacedGuess = string.Join(" ",
                        guess.ToCharArray());
                    string spacedResult = string.Join(" ",
                        result.ToCharArray());

                    // pad the result side so columns line up
                    spacedResult = spacedResult
                        .PadRight(k_ResultPrintPadding);

                    Console.WriteLine(
                        "| " + spacedGuess +
                        " |" + spacedResult + "|");
                }

                Console.WriteLine("|=========|=======|");
            }
        }
    }
}