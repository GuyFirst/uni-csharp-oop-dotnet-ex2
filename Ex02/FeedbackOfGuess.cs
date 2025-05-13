using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    public struct FeedbackOfGuess
    {
        public enum FeedbackOfGuessType { ExactPlace, WrongPlace, NotInGuess }
        public FeedbackOfGuessType[] feedbackOfGuessTypes;

        public FeedbackOfGuess(FeedbackOfGuessType[] i_FeedbackOfGuessTypes)
        {
            feedbackOfGuessTypes = i_FeedbackOfGuessTypes;
        }


    }
}
