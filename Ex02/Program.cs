using System;

namespace Ex02
{
    public class Program
    {
        public static void Main()
        {
           InputValidator validator = new InputValidator();

            if (!validator.IsInputValid(Console.ReadLine()))
            {
                Console.WriteLine(validator.MessegeToShowTheUser);
            }
        }
    }
}