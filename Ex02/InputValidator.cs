using System;


namespace Ex02
{
    public class InputValidator
    {
        private const int k_NumberOfValidCharactersInUserGuess = 4;
        private const int k_NumberOfUniqueLetters = 8;
        public string MessegeToShowTheUser {  get; private set; }
        public bool IsInputValid(string i_Input)
        {
            MessegeToShowTheUser = string.Empty;
            bool isInputValid = true;

            if (!isCorrectLength(i_Input))
            {
                MessegeToShowTheUser = "You did not submit 4 characters.\nPlease submit exactly 4 unique letters between A and H.";
                isInputValid = false;
            }
            else if (!areCharactersValidAndUnique(i_Input))
            {
                MessegeToShowTheUser += "\nPlease submit exactly 4 unique letters between A and H.";
                isInputValid = false;
            }

            return isInputValid;
        }

        private bool isCorrectLength(string i_Input)
        {
            bool isCorrectLength = i_Input.Length == k_NumberOfValidCharactersInUserGuess;
            return isCorrectLength;
        }

        private bool areCharactersValidAndUnique(string i_Input)
        {
            bool areCharactersValidAndUnique = true;
            bool[] uniqueLetters = new bool[k_NumberOfUniqueLetters];

            foreach (char currentCharacter in i_Input)
            {
                char upperChar = char.ToUpper(currentCharacter);

                if (!isLetterBetweenAAndH(upperChar))
                {
                    MessegeToShowTheUser = !char.IsLetter(currentCharacter)
                        ? "You entered non letter characters."
                        : "You entered a character which is a letter beyond the boundary between A and H.";
                    areCharactersValidAndUnique = false;
                    break;
                }

                int index = upperChar - 'A';
                if (uniqueLetters[index])
                {
                    MessegeToShowTheUser = "Each letter must be UNIQUE.";
                    areCharactersValidAndUnique = false;
                    break;
                }

                uniqueLetters[index] = true;
            }

            return areCharactersValidAndUnique;
        }

        private bool isLetterBetweenAAndH(char i_CurrentCharacter)
        {
            bool isLetterBetweenAAndH = i_CurrentCharacter >= 'A' && i_CurrentCharacter <= 'H';
            return isLetterBetweenAAndH;
        }

    }
}
