using System;
namespace encryptWord
{
    /// <summary>
    /// This class simulates a guessing game. The user inputs a word and
    /// an encrypted version is output. The user can then guess the number
    /// of letters the original word has been shifted, or they can
    /// just quit. This game can be played as many times as the user
    /// wishes. The input word must be at least four letters long and must
    /// be letters. The assumption is that letters will be input. There
    /// is a check to ensure that when the user guesses only integers
    /// are input.
    /// </summary>
    public class driver
    {
        public driver()
        {
            GuessingGame firstGame = new GuessingGame();

            String word; //entered by user

            String encryptedOutput; //to tell the user what the encrypted word is

            String guessInput; //to hold user guesses

            int guessNum; //to check if guess is correct

            int shiftNum; //to check if guess is correct

            int exitLoop = -1; //to exit loop

            String tellUserIfGuessIsRight; //to inform user if guess it right

            bool status; //to show that states are correct

            Console.WriteLine("Please enter a word to be encrypted\n");

            word = Console.ReadLine(); //to start the game

            encryptedOutput = firstGame.encryptWord.encrypt(word); //stores encrypted word to be output

            shiftNum = firstGame.encryptWord.getShiftNum(); //for guessing

            Console.WriteLine("\nThe encrypted word is: " + encryptedOutput);

            status = firstGame.encryptWord.getStatus(); //to check that states are correct

            Console.WriteLine("\nStatus of encryptWord(should be true): " + status);

            Console.WriteLine("\nEnter an integer to guess the shift or\n" +
                              "enter -1 to quit\n");

            guessInput = Console.ReadLine(); //for guessing

            //To ensure input is valid
            while (!Int32.TryParse(guessInput, out guessNum))
            {
                Console.WriteLine("Please enter valid input: \n");

                guessInput = Console.ReadLine();
            }

            //lets the user guess until correct or doesn't want to anymore
            while (guessNum != exitLoop)
            {
                tellUserIfGuessIsRight = firstGame.checkGuess(guessNum); //to show user if guess is correct

                Console.WriteLine(tellUserIfGuessIsRight); //to tell user if guess is correct

                //if guess is incorrect, lets user keep guessing
                while (guessNum != shiftNum)
                {
                    Console.WriteLine("\nGuess Again? (enter -1 to quit)\n");

                    guessInput = Console.ReadLine();

                    //checks if input is valid
                    while (!Int32.TryParse(guessInput, out guessNum))
                    {
                        Console.WriteLine("Please enter valid input: \n");

                        guessInput = Console.ReadLine();
                    }

                    tellUserIfGuessIsRight = firstGame.checkGuess(guessNum);

                    Console.WriteLine(tellUserIfGuessIsRight);

                }

                firstGame.getStats(); //to show stats after guessing

                String decodedWord = firstGame.encryptWord.decode();

                Console.WriteLine("\nThe decoded word is: " + decodedWord);

                Console.WriteLine("\nPlay Again? y or n");

                word = Console.ReadLine();

                //to let the user play again
                if (word.Equals("y"))
                {
                    firstGame.resetGame(); //to clear the old info

                    status = firstGame.encryptWord.getStatus(); //to check that getStatus worked

                    Console.WriteLine("\nStatus of encryptWord(should be false): " + status);

                    Console.WriteLine("\nPlease enter a word to be encrypted\n");

                    word = Console.ReadLine();

                    encryptedOutput = firstGame.encryptWord.encrypt(word); //stores encrypted word to be output

                    status = firstGame.encryptWord.getStatus(); //to check states are correct

                    Console.WriteLine("\nStatus of encryptWord(should be true): " + status);

                    shiftNum = firstGame.encryptWord.getShiftNum();

                    Console.WriteLine("The encrypted word is: " + encryptedOutput + "\n\n");

                    Console.WriteLine("Enter an integer to guess the shift or\n" +
                                      "enter -1 to quit\n");

                    guessInput = Console.ReadLine();

                    //To ensure input is valid
                    while (!Int32.TryParse(guessInput, out guessNum))
                    {
                        Console.WriteLine("Please enter valid input: \n");

                        guessInput = Console.ReadLine();
                    }
                }
                else
                {
                    guessNum = exitLoop;
                }
            }

            Console.WriteLine("Thanks for playing");
        }

    }
            
}
