using System.Collections.Generic;

namespace Ex02
{
    public class HistoryOfGuesses
    {
        public List<RowOfGuesses> RowOfGuesses { get; } = new List<RowOfGuesses>();

        public void AddNewRow(GuessHandler i_Guess, FeedbackOfGuess i_Feedback)
        {
            RowOfGuesses.Add(new RowOfGuesses(i_Guess, i_Feedback));
        }
    }
}