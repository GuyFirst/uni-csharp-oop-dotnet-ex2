using System;
using System.Linq;
using System.Text;

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
            int numberOfGuesses = ConsoleUI.AskUserToEnterNumberOfGuesses();
            string secretString = SecretWordGenerator.GenerateSecretWord();
            GuessHandler secretWord = ConsoleUI.ConvertStringToGuessHandler(secretString);

            GameData gameData = new GameData(secretString, numberOfGuesses);
            gameData.SecretWord = secretWord;

            while (gameData.RemainingNumberOfGuesses > 0 && !WinFlag && !QuittingGameFlag)
            {
                ConsoleUI.ShowBoard(gameData.HistoryOfGuesses, gameData.r_MaxUserGuesses);

                bool userWantsToQuit;
                GuessHandler userGuess = ConsoleUI.ReadGuessFromUser(out userWantsToQuit);
                QuittingGameFlag = userWantsToQuit;

                if (QuittingGameFlag)
                {
                    break;
                }

                FeedbackOfGuess feedback = calculateFeedbackOnGuess(userGuess, gameData.SecretWord);
                gameData.AddGuessAndFeedback(userGuess, feedback);

                WinFlag = checkIfGuessCorrect(feedback);
                gameData.RemainingNumberOfGuesses--;
            }

            // Reveal secret if user wins or loses
            GuessHandler secretWordToReveal =
                (WinFlag || (!WinFlag && !QuittingGameFlag && gameData.RemainingNumberOfGuesses == 0))
                ? gameData.SecretWord
                : default;

            ConsoleUI.ShowBoard(gameData.HistoryOfGuesses, gameData.r_MaxUserGuesses, secretWordToReveal);

            bool playAgain = manageEndOfGame(gameData.r_MaxUserGuesses, gameData.RemainingNumberOfGuesses);
            if (!playAgain)
            {
                QuittingGameFlag = true;
            }
        }

        private FeedbackOfGuess calculateFeedbackOnGuess(GuessHandler i_UserGuess, GuessHandler i_SecretCode)
        {
            int guessLength = i_UserGuess.Guess.Count;
            FeedbackOfGuess.FeedbackOfGuessType[] feedback = new FeedbackOfGuess.FeedbackOfGuessType[guessLength];

            int i = 0;
            foreach (var guessLetter in i_UserGuess.Guess)
            {
                feedback[i] = FeedbackOfGuess.FeedbackOfGuessType.NotInGuess;

                int j = 0;
                foreach (var secretLetter in i_SecretCode.Guess)
                {
                    if (guessLetter == secretLetter)
                    {
                        if (i == j)
                        {
                            feedback[i] = FeedbackOfGuess.FeedbackOfGuessType.ExactPlace;
                        }
                        else
                        {
                            feedback[i] = FeedbackOfGuess.FeedbackOfGuessType.WrongPlace;
                        }

                        break;
                    }

                    j++;
                }

                i++;
            }

            return new FeedbackOfGuess(feedback);
        }

        private bool checkIfGuessCorrect(FeedbackOfGuess i_Feedback)
        {
            bool isGuessCorrect = true;

            for (int i = 0; i < i_Feedback.feedbackOfGuessTypes.Length; i++)
            {
                if (i_Feedback.feedbackOfGuessTypes[i] != FeedbackOfGuess.FeedbackOfGuessType.ExactPlace)
                {
                    isGuessCorrect = false;
                    break;
                }
            }

            return isGuessCorrect;
        }

        private bool manageEndOfGame(int i_StartingNumberOfGuess, int i_NumberOfGuessingRemained)
        {
            bool shouldStartNewGame;

            if (WinFlag)
            {
                shouldStartNewGame = ConsoleUI.PrintWinMessage(i_StartingNumberOfGuess, i_NumberOfGuessingRemained);
            }
            else if (!QuittingGameFlag)
            {
                shouldStartNewGame = ConsoleUI.PrintLoseMessage();
            }
            else
            {
                shouldStartNewGame = ConsoleUI.PrintQuitMessage();
            }

            return shouldStartNewGame;
        }

    }
}