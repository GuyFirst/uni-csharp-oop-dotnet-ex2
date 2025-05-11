using System;


namespace Ex02
{
    public class GameManager
    {
        public bool QuittingGameFlag { get; set; }
        public bool WinFlag { get; set; }
        public bool LoseFlag { get; set; }

        public void StartNewGame()
        {
            while (true)
            {
                runGame();
                
                //getting the response from consoleUI if the user wants to play a new game

                if(QuittingGameFlag)
                {
                    return;
                }
            }
        }
        
        
        private void runGame()
        {
            int numberOfGuesses = ConsoleUI.AskUserToEnterNumberOfGuesses();
            string secretWord = SecretWordGenerator.GenerateSecretWord();
            GameState gameState = new GameState(secretWord, numberOfGuesses);
            GuessAttempt guessAttempt = new GuessAttempt(secretWord);
            string userGuess = ConsoleUI.AskFromUserToTakeAGuess(out bool o_GameOverFlag);
           
            bool stopTheGame = WinFlag || LoseFlag;

            while (!stopTheGame) 
            {
                string feedbackOnGuess = guessAttempt.GiveFeedbackOnGuess(userGuess);
                if (feedbackOnGuess == "VVVV")
                {
                    winningFlag = true;
                }
                gameState.addGuessAndFeedback(userGuess, feedbackOnGuess);
                ConsoleUI.PrintBoard(gameState.GuessesAndResultsHistory, numberOfGuesses);
                if(!stopTheGame)
                {
                    userGuess = ConsoleUI.AskFromUserToTakeAGuess(out o_GameOverFlag);
                }      
            }

          
            //return



        }







    }


    
     
}
