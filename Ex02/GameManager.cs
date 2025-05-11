using System;

namespace Ex02
{
    public class GameManager
    {
        public bool QuittingGameFlag { get; set; }
        public bool WinFlag { get; set; }

        public void StartNewGame()
        {
            while (true)
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
            GameData currentGameData = new GameData(secretWord, ConsoleUI.AskUserToEnterNumberOfGuesses());
            GuessAttempt activeGuessAttempt = new GuessAttempt(secretWord);
            string userInputGuess = ConsoleUI.AskFromUserToTakeAGuess(out bool o_UserDecidedToQuit);
            bool hasGameEnded = (WinFlag || currentGameData.m_RemaningNumberOfGuesses <= 0);
            
            while (!hasGameEnded) 
            {
                string feedbackOnGuess = activeGuessAttempt.GiveFeedbackOnGuess(userInputGuess);

                currentGameData.AddGuessAndFeedback(userInputGuess, feedbackOnGuess, currentGameData.m_RemaningNumberOfGuesses);
                WinFlag = (feedbackOnGuess.Equals(k_WinnerResult));
                hasGameEnded = WinFlag || (--currentGameData.m_RemaningNumberOfGuesses <= 0);
                ConsoleUI.PrintBoard(currentGameData.GuessesAndResultsHistory, currentGameData.r_MaxUserGuesses);
                if(!hasGameEnded)
                {
                    userInputGuess = ConsoleUI.AskFromUserToTakeAGuess(out o_UserDecidedToQuit);
                }      
            }

            QuittingGameFlag = o_UserDecidedToQuit;
            manageEndOfGame(currentGameData.r_MaxUserGuesses, currentGameData.m_RemaningNumberOfGuesses);
        }

        private void manageEndOfGame(int i_StartingNumberOfGuess, int i_NumberOfGuessingRemained)
        {
            if (WinFlag)
            {
                //console greet winner
            }
            else if (!QuittingGameFlag)
            {
                //console tell user he lost
            }
            else
            {
                //tell user goodbye 
            }

            //ask the user if he wants to play another game IF HE HAVENT DECIDED TO QUIT YET
        }





    }


    
     
}
