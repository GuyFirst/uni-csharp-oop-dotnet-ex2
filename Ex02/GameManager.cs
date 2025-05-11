using System;


namespace Ex02
{
    public class GameManager
    {
        public bool QuittingGameFlag { get; set; }

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
            //create game state



            //gameloop
            {

                // if user enters q - quit the game



            }

          
            //return



        }







    }


    
     
}
