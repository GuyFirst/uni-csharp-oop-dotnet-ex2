namespace Ex02
{
    public struct FeedbackOfGuess
    {
        public enum eFeedbackOfGuessType { ExactPlace, WrongPlace, NotInGuess }
        public eFeedbackOfGuessType[] m_FeedbackOfGuessTypes;

        public FeedbackOfGuess(eFeedbackOfGuessType[] i_FeedbackOfGuessTypes)
        {
            m_FeedbackOfGuessTypes = i_FeedbackOfGuessTypes;
        }
    }
}