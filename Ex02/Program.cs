using System;

namespace Ex02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TestGuess("HFGC", "ABCD"); // Expect: "x   " (C is common, different position)
            TestGuess("ABCD", "ABCD"); // Expect: "vvvv" (All correct positions)
            TestGuess("DCBA", "ABCD"); // Expect: "xxxx" (All correct letters, wrong positions)
            TestGuess("HFBD", "ABCD"); // Expect: "VX  " (one matches)
        }

        public static void TestGuess(string secret, string guess)
        {
            GuessAttempt attempt = new GuessAttempt(secret);
            string feedback = attempt.GiveFeedbackOnGuess(guess);
            Console.WriteLine($"Secret: {secret}, Guess: {guess} → Feedback: \"{feedback}\"");
        }
    }
}
