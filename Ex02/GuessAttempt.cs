using System;

namespace Ex02
{
    public enum LetterFeedback
    {
        Wrong,
        RightLetterWrongSpot,
        CorrectSpot
    }

    public class GuessAttempt<TSymbol>
    {
        public TSymbol[] Guess { get; private set; }
        public LetterFeedback[] Feedback { get; private set; }

        public GuessAttempt(TSymbol[] i_Guess, LetterFeedback[] i_Feedback)
        {
            this.Guess = i_Guess;
            this.Feedback = i_Feedback;
        }
    }
}