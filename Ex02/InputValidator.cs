namespace Ex02
{
    public class InputValidator
    {
        private const int k_NumberOfValidCharactersInUserGuess = 4;
        private const int k_NumberOfUniqueLetters = 8;
        public string ReasonOfBadInput { get; private set; }

        public bool IsInputValid(string i_Input, out bool io_UserDecidedToQuit)
        {
            io_UserDecidedToQuit = false;
            ReasonOfBadInput = string.Empty;
            bool isInputValid;

            if (i_Input == "Q")
            {
                io_UserDecidedToQuit = true;
                isInputValid = true;
            }
            else if (i_Input.Length != k_NumberOfValidCharactersInUserGuess)
            {
                ReasonOfBadInput = "You did not submit 4 characters.\nPlease submit exactly 4 unique uppercase letters between A and H.";
                isInputValid = false;
            }
            else if (!areCharactersValidAndUnique(i_Input))
            {
                ReasonOfBadInput += "\nPlease submit exactly 4 unique uppercase letters between A and H.";
                isInputValid = false;
            }
            else
            {
                isInputValid = true;
            }

            return isInputValid;
        }

        private bool areCharactersValidAndUnique(string i_Input)
        {
            bool areCharactersValidAndUnique;
            bool[] uniqueLetters = new bool[k_NumberOfUniqueLetters];
            ReasonOfBadInput = string.Empty;

            foreach (char currentCharacter in i_Input)
            {
                if (!char.IsLetter(currentCharacter))
                {
                    ReasonOfBadInput = "You entered non-letter characters.";
                    areCharactersValidAndUnique = false;
                    break;
                }

                if (!char.IsUpper(currentCharacter))
                {
                    ReasonOfBadInput = "All characters must be uppercase letters between A and H.";
                    areCharactersValidAndUnique = false;
                    break;
                }

                if (currentCharacter < 'A' || currentCharacter > 'H')
                {
                    ReasonOfBadInput = "You entered a character outside the range A-H.";
                    areCharactersValidAndUnique = false;
                    break;
                }

                int index = currentCharacter - 'A';

                if (uniqueLetters[index])
                {
                    ReasonOfBadInput = "Each letter must be UNIQUE.";
                    areCharactersValidAndUnique = false;
                    break;
                }

                uniqueLetters[index] = true;
            }

            areCharactersValidAndUnique = (ReasonOfBadInput == string.Empty);

            return areCharactersValidAndUnique;
        }
    }
}
