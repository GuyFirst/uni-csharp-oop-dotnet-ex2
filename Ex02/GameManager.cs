using System;

namespace Ex02
{
    public class GameManager
    {
        public bool QuittingGameFlag { get; set; }

        public bool WinFlag { get; set; }

        public void StartNewGame()
        {
            while(true)
            {
                runGame();
                if(QuittingGameFlag)
                {
                    break;
                }
            }
        }

        private void runGame()
        {
            const string k_WinnerResult = "VVVV";
            string secretWord = SecretWordGenerator.GenerateSecretWord();

            ConsoleUtils.Screen.Clear();
            GameData currentGameData = new GameData(secretWord, ConsoleUI.AskUserToEnterNumberOfGuesses());
            GuessAttempt activeGuessAttempt = new GuessAttempt(secretWord);

            ConsoleUI.PrintBoard(currentGameData.GuessesAndResultsHistory, currentGameData.r_MaxUserGuesses);
            string userInputGuess = ConsoleUI.AskFromUserToTakeAGuess(out bool o_UserDecidedToQuit);
            bool hasGameEnded = (WinFlag || currentGameData.RemainingNumberOfGuesses <= 0);

            while (!hasGameEnded)
            {
                string feedbackOnGuess = activeGuessAttempt.GiveFeedbackOnGuess(userInputGuess);

                currentGameData.AddGuessAndFeedback(userInputGuess, feedbackOnGuess);
                ConsoleUI.PrintBoard(currentGameData.GuessesAndResultsHistory, currentGameData.r_MaxUserGuesses);
                WinFlag = (feedbackOnGuess.Equals(k_WinnerResult));
                hasGameEnded = (--currentGameData.RemainingNumberOfGuesses <= 0) || WinFlag;
                if (!hasGameEnded)
                {
                    userInputGuess = ConsoleUI.AskFromUserToTakeAGuess(out o_UserDecidedToQuit);
                }
            }

            QuittingGameFlag = o_UserDecidedToQuit;

            // Reveal the secret word when the game ends
            ConsoleUI.PrintBoard(currentGameData.GuessesAndResultsHistory, currentGameData.r_MaxUserGuesses, secretWord);

            bool playAgain = manageEndOfGame(
                currentGameData.r_MaxUserGuesses,
                currentGameData.RemainingNumberOfGuesses);

            if (!playAgain)
            {
                QuittingGameFlag = true;
            }
        }

        private bool manageEndOfGame(int i_StartingNumberOfGuess, int i_NumberOfGuessingRemained)
        {
            if(WinFlag)
            {
                return ConsoleUI.PrintWinMessage(i_StartingNumberOfGuess, i_NumberOfGuessingRemained);
            }
            else if(!QuittingGameFlag)
            {
                return ConsoleUI.PrintLoseMessage();
            }
            else
            {
                return ConsoleUI.PrintQuitMessage();
            }
        }
    }
}