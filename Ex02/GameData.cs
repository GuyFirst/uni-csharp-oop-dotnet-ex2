namespace Ex02
{
    public class GameData
    {   
        public readonly int r_MaxUserGuesses;
        public int RemainingNumberOfGuesses { get; set; }
        public GuessHandler SecretWord { get; set; }
        public HistoryOfGuesses HistoryOfGuesses { get; set; }

        public GameData(string i_SecretWord, int i_MaxUserGuesses) 
        {
            SecretWord = new GuessHandler();
            HistoryOfGuesses = new HistoryOfGuesses();
            r_MaxUserGuesses = i_MaxUserGuesses;
            RemainingNumberOfGuesses = i_MaxUserGuesses;
        }

        public void AddGuessAndFeedback(GuessHandler i_Guess, FeedbackOfGuess i_Feedback)
        {
            HistoryOfGuesses.AddNewRow(i_Guess, i_Feedback);
        }
    }
}