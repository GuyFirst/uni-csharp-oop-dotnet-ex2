using Ex02;
using System;

namespace Ex02
{
    public enum GameState
    {
        InProgress,
        Won,
        Lost
    }

    public class GameManager<TSymbol>
    {
        private TSymbol[] m_Secret;
        private int m_RemainingGuesses;
        private int m_MaxGuesses;

        public GameState State { get; private set; }

        public int MaxGuesses
        {
            get { return m_MaxGuesses; }
        }

        public int RemainingGuesses
        {
            get { return m_RemainingGuesses; }
        }

        public int WordLength
        {
            get { return m_Secret.Length; }
        }

        public void Initialize(TSymbol[] i_Secret, int i_MaxGuesses)
        {
            this.m_Secret = i_Secret;
            this.m_MaxGuesses = i_MaxGuesses;
            this.m_RemainingGuesses = i_MaxGuesses;
            this.State = GameState.InProgress;
        }

        public GuessAttempt<TSymbol> SubmitGuess(TSymbol[] i_Guess)
        {
            if (i_Guess.Length != m_Secret.Length)
            {
                throw new ArgumentException("Guess length must match secret length.");
            }

            LetterFeedback[] feedback = new LetterFeedback[i_Guess.Length];
            bool[] secretMatched = new bool[i_Guess.Length];
            bool[] guessMatched = new bool[i_Guess.Length];

            // 1) Exact matches
            for (int i = 0; i < i_Guess.Length; i++)
            {
                if (i_Guess[i].Equals(m_Secret[i]))
                {
                    feedback[i] = LetterFeedback.CorrectSpot;
                    secretMatched[i] = true;
                    guessMatched[i] = true;
                }
            }

            // 2) Right letter, wrong spot
            for (int i = 0; i < i_Guess.Length; i++)
            {
                if (guessMatched[i] == false)
                {
                    bool found = false;
                    for (int j = 0; j < m_Secret.Length; j++)
                    {
                        if (secretMatched[j] == false && i_Guess[i].Equals(m_Secret[j]))
                        {
                            found = true;
                            secretMatched[j] = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        feedback[i] = LetterFeedback.RightLetterWrongSpot;
                    }
                    else
                    {
                        feedback[i] = LetterFeedback.Wrong;
                    }
                }
            }

            m_RemainingGuesses--;

            if (allCorrect(feedback))
            {
                this.State = GameState.Won;
            }
            else if (m_RemainingGuesses <= 0)
            {
                this.State = GameState.Lost;
            }

            return new GuessAttempt<TSymbol>(i_Guess, feedback);
        }

        private bool allCorrect(LetterFeedback[] i_Feedback)
        {
            foreach(LetterFeedback positionInFeedback in i_Feedback)
            {
                if (positionInFeedback != LetterFeedback.CorrectSpot)
                {
                    return false;
                }
            }

            return true;
        }
    }
}