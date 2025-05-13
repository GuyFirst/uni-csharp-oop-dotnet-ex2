using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public struct RowOfGuesses
    {
        public GuessHandler UserGuess { get; }
        public FeedbackOfGuess Feedback { get; }

        public RowOfGuesses(GuessHandler i_UserGuess, FeedbackOfGuess i_Feedback)
        {
            UserGuess = i_UserGuess;
            Feedback = i_Feedback;
        }
    }

}
