namespace Ex02
{
    public class InputValidator
    {
        private const int k_NumberOfValidCharactersInUserGuess = 4;
        private const int k_NumberOfUniqueLetters = 8;
        public string ReasonOfBadInput {  get; private set; } //TODO decide if changing to const
        public bool IsInputValid(string i_Input, out bool io_UserDecidedToQuit)
        {
            io_UserDecidedToQuit = false;
            ReasonOfBadInput = string.Empty;
            bool isInputValid = true;

            if(i_Input == "Q")
            {
                io_UserDecidedToQuit = true;
            }
            else if (i_Input.Length != k_NumberOfValidCharactersInUserGuess)
            {
                ReasonOfBadInput = "You did not submit 4 characters.\nPlease submit exactly 4 unique letters between A and H.";
                isInputValid = false;
            }
            else if (!areCharactersValidAndUnique(i_Input))
            {
                ReasonOfBadInput += "\nPlease submit exactly 4 unique letters between A and H.";
                isInputValid = false;
            }

            return isInputValid;
        }

        private bool areCharactersValidAndUnique(string i_Input)
        {
            bool  areCharactersValidAndUnique = true;
            bool[] uniqueLetters = new bool[k_NumberOfUniqueLetters];

            foreach (char currentCharacter in i_Input)
            {

                if (!(currentCharacter >= 'A' && currentCharacter <= 'H'))
                {
                    ReasonOfBadInput = !char.IsLetter(currentCharacter)
                        ? "You entered non letter characters."
                        : "You entered a character which is a letter beyond the boundary between A and H.";
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

            return areCharactersValidAndUnique;
        }
    }
}