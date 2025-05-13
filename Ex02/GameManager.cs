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
        private TSymbol[] m_secret;
        private int m_remainingGuesses;
        private int m_maxGuesses;

        public GameState State { get; private set; }

        public int MaxGuesses
        {
            get { return m_maxGuesses; }
        }

        public int RemainingGuesses
        {
            get { return m_remainingGuesses; }
        }

        public int WordLength
        {
            get { return m_secret.Length; }
        }

        public void Initialize(TSymbol[] secret, int maxGuesses)
        {
            this.m_secret = secret;
            this.m_maxGuesses = maxGuesses;
            this.m_remainingGuesses = maxGuesses;
            this.State = GameState.InProgress;
        }

        public GuessAttempt<TSymbol> SubmitGuess(TSymbol[] guess)
        {
            if (guess.Length != m_secret.Length)
            {
                throw new ArgumentException("Guess length must match secret length.");
            }

            LetterFeedback[] feedback = new LetterFeedback[guess.Length];
            bool[] secretMatched = new bool[guess.Length];
            bool[] guessMatched = new bool[guess.Length];

            // 1) Exact matches
            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i].Equals(m_secret[i]))
                {
                    feedback[i] = LetterFeedback.CorrectSpot;
                    secretMatched[i] = true;
                    guessMatched[i] = true;
                }
            }

            // 2) Right letter, wrong spot
            for (int i = 0; i < guess.Length; i++)
            {
                if (guessMatched[i] == false)
                {
                    bool found = false;
                    for (int j = 0; j < m_secret.Length; j++)
                    {
                        if (secretMatched[j] == false && guess[i].Equals(m_secret[j]))
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

            m_remainingGuesses--;

            if (AllCorrect(feedback))
            {
                this.State = GameState.Won;
            }
            else if (m_remainingGuesses <= 0)
            {
                this.State = GameState.Lost;
            }

            return new GuessAttempt<TSymbol>(guess, feedback);
        }

        private bool AllCorrect(LetterFeedback[] feedback)
        {
            for (int i = 0; i < feedback.Length; i++)
            {
                if (feedback[i] != LetterFeedback.CorrectSpot)
                {
                    return false;
                }
            }

            return true;
        }
    }
}