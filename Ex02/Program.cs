using Ex02.ConsoleUtils;
using System;
using System.Collections.Generic;

namespace Ex02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Keep running new games as long as RunGame returns true
            while (RunGame())
            {

            }

            Console.WriteLine("Thanks for playing! Goodbye.");
        }

        private static bool RunGame()
        {
            // 1) Ask for number of tries
            int maxGuesses;
            Console.Write("Please provide your number of guesses between 4 and 10: ");
            while (true)
            {
                string raw = Console.ReadLine();
                if (int.TryParse(raw, out maxGuesses)
                    && maxGuesses >= 4
                    && maxGuesses <= 10)
                {
                    break;
                }
                Console.Write("Invalid—enter an integer between 4 and 10: ");
            }

            // 2) Prepare empty board and display it
            string[,] gameHistory = new string[maxGuesses, 2];
            ConsoleUI.PrintBoard(gameHistory, maxGuesses);

            // 3) Generate secret and initialize engine
            int wordLength = 4;
            List<char> allowed = new List<char>
                { 'A','B','C','D','E','F','G','H' };
            InputValidator validator =
                new InputValidator(wordLength, allowed);
            var generator = new SecretWordGenerator(allowed, new Random());
            char[] secret = generator.Generate(wordLength);

            var engine = new GameManager<char>();
            engine.Initialize(secret, maxGuesses);


            // 4) Main guessing loop
            bool quitRequested = false;
            int turn = 0;
            while (engine.State == GameState.InProgress)
            {
                Console.WriteLine("Enter your next guess (or 'Q' to quit): ");
                string guessInput = Console.ReadLine();

                // Q => quit
                if (string.Equals(guessInput, "Q"))
                {
                    quitRequested = true;
                    break;
                }

                // length check
                if (!validator.Validate(guessInput))
                {
                    continue;
                }

                // submit to core
                var attempt = engine.SubmitGuess(guessInput.ToCharArray());

                // build feedback: V, then X, then spaces
                char[] feedbackArr = new char[wordLength];
                int idx = 0;
                foreach (var f in attempt.Feedback)
                {
                    if (f == LetterFeedback.CorrectSpot)
                        feedbackArr[idx++] = 'V';
                }
                foreach (var f in attempt.Feedback)
                {
                    if (f == LetterFeedback.RightLetterWrongSpot)
                        feedbackArr[idx++] = 'X';
                }
                while (idx < wordLength)
                    feedbackArr[idx++] = ' ';

                // record and redraw
                gameHistory[turn, GameData.k_Guess] = guessInput;
                gameHistory[turn, GameData.k_Result] = new string(feedbackArr);
                turn++;
                ConsoleUI.PrintBoard(gameHistory, maxGuesses);
            }

            // 5) Final reveal & message
            ConsoleUI.PrintBoard(gameHistory, maxGuesses, new string(secret));

            if (quitRequested)
            {
                Console.WriteLine("You quit the game. The secret was: " + new string(secret));
                return false;   // do not prompt to play again
            }
            else if (engine.State == GameState.Won)
            {
                Console.WriteLine($"Congratulations! You guessed after {turn} steps!");
            }
            else
            {
                Console.WriteLine("No more guesses allowed. You Lost!");
            }

            // 6) Ask to play again
            Console.Write("Would you like to play again? (Y/N): ");
            string answer = Console.ReadLine();
            return string.Equals(answer, "Y", StringComparison.OrdinalIgnoreCase);
        }
    }
}