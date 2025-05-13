using System;
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
            //implementtttttttttttttttttttttttttttttttttttttt
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