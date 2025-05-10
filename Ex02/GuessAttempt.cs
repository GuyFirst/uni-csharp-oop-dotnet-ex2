using System;

namespace Ex02
{
    public class GuessAttempt
    {
        public string SecretCode { get; set; }
        public string FeedbackResult { get; set; } // the [v,x,vv] kind of stuff

        public GuessAttempt(string i_SecretCode)
        {
            SecretCode = i_SecretCode;
        }

        public string GiveFeedbackOnGuess(string i_UserGuess)
        {




            return FeedbackResult;
        }
    }
}
