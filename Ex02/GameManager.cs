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
            int numberOfGuessesRemaining = ConsoleUI.AskUserToEnterNumberOfGuesses();
            string secretWord = SecretWordGenerator.GenerateSecretWord();
            GameData currentGameData = new GameData(secretWord, numberOfGuessesRemaining);
            GuessAttempt ActiveGuessAttempt = new GuessAttempt(secretWord);
            string userInputGuess = ConsoleUI.AskFromUserToTakeAGuess(out bool o_UserDecidedToQuit);
            bool HasGameEnded = WinFlag || numberOfGuessesRemaining <= 0;

            while (!HasGameEnded) 
            {
                string feedbackOnGuess = ActiveGuessAttempt.GiveFeedbackOnGuess(userInputGuess);

                WinFlag = (feedbackOnGuess == "VVVV");
                HasGameEnded = WinFlag || (--numberOfGuessesRemaining <= 0);
                currentGameData.addGuessAndFeedback(userInputGuess, feedbackOnGuess);
                ConsoleUI.PrintBoard(currentGameData.GuessesAndResultsHistory, currentGameData.r_MaxUserGuesses);
                if(!HasGameEnded)
                {
                    userInputGuess = ConsoleUI.AskFromUserToTakeAGuess(out o_UserDecidedToQuit);
                }      
            }

            QuittingGameFlag = o_UserDecidedToQuit;
            manageEndOfGame(currentGameData.r_MaxUserGuesses, numberOfGuessesRemaining);
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
