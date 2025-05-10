using System;
using System.Text;

namespace Ex02
{
    public class GuessAttempt
    {
        public string SecretCode { get; set; }
       

        public GuessAttempt(string i_SecretCode)
        {
            SecretCode = i_SecretCode;
        }
        public string GiveFeedbackOnGuess(string i_UserGuess)
        {
            int countV = 0;
            int countX = 0;

            for (int i = 0; i < i_UserGuess.Length; ++i)
            {
                for (int j = 0; j < SecretCode.Length; ++j)
                {
                    if (i_UserGuess[i] == SecretCode[j])
                    {
                        if (i == j)
                        {
                            countV++;
                        }            
                        else
                        {
                            countX++;
                        }

                        break;
                    }
                }
            }

            StringBuilder feedback = new StringBuilder();
            feedback.Append('V', countV);
            feedback.Append('X', countX);
            feedback.Append(' ', i_UserGuess.Length - countV - countX);

            return feedback.ToString();
        }

    }
}
