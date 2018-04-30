// AUTHOR: Chris Choi
// FILENAME: encryptWord.cs
// Date: 4/15/18
// REVISION HISTORY: v1.0


using System;

namespace encryptWord
{
    /// <summary>
    /// This class shifts each letter of a user-passed in word up n letters,
    /// where n is between 1 and 6, and returns that word. 
    /// Only strings that are at least 4 letters long are allowed to be 
    /// encrypted. No other input is allowed.
    /// States are indicated by the status variable, where
    /// true means that an encrypted word is stored. Words can be entered
    /// in any combination of upper and lower case, but will be converted
    /// to upper case in the class.
    /// </summary>
    class EncryptWord
    {
        private const int MIN_SHIFT = 1; //So letters shift at least 1

        private const int MAX_SHIFT = 7; //So the letters dont shift too much

        private string word = String.Empty; //holds the word

        private string encryptedWord = String.Empty; //holds the encrypted word

        private int shiftNum = 0;

        private bool status = false; //false means encrypt word is off

        private Random random = new Random(); //for shift number

        /// Encrypt the specified passedWord.
        /// This sets a random shift number between 1 and 6, encrypts the word
        /// that many letters, changes the status afterwords, and stores
        /// the encrypted and non-encrypted words.
        /// <returns>The encrypted word.</returns>
        /// <param name="passedWord">Passed word.</param>
        /// precondition: none
        /// postcondition: status becomes true, word and encrypted word
        /// will have values, a shiftNum will be generated
        public string encrypt(string passedWord)
        {
            int minWordLength = 4;

            //to ensure word length minimum
            while (passedWord.Length < minWordLength)
            {
                Console.WriteLine("word is too short, enter new word\n");

                passedWord = Console.ReadLine();
            }

            const int upperBound = 90; //upper bound of valid letters using ascII

            const int lowerBound = 64; //lower bound of valid letters using ascII

            word = passedWord;

            word = word.ToUpper(); //because everything is uppercase in this program

            int wordLength = word.Length; //to loop through and shift letters

            shiftNum = random.Next(MIN_SHIFT, MAX_SHIFT);

            status = true; //because a word is now stored and soon to be encrypted

            char[] toBeEncrypted = new char[wordLength]; //to shift letters

            //to shift letters
            for (int i = 0; i < wordLength; i++)
            {
                toBeEncrypted[i] = word[i];

            }

            for (int i = 0; i < wordLength; i++)
            {
                toBeEncrypted[i] = (char)(toBeEncrypted[i] + shiftNum);

                //to make sure only letters are used
                if ((int)toBeEncrypted[i] > upperBound)
                {
                    toBeEncrypted[i] = (char)((toBeEncrypted[i] - upperBound) + lowerBound);
                }

            }

            encryptedWord = new string(toBeEncrypted);

            return encryptedWord;
        }

        /// Gets the status.
        /// Use to check if word is encrypted or not
        /// <returns>The status.</returns>
        public bool getStatus()
        {
            return status;
        }

        /// <summary>
        /// "decodes" the encrypted word by returning the original
        /// </summary>
        /// <returns>The decode.</returns>
        /// precondition: status should be true
        /// postcondition: status will be false
        public string decode()
        {
            status = false;

            return word;
        }
        /// <summary>
        /// Resets the encrypt word.
        /// </summary>
        /// precondition: none
        /// postcondition: status will be false and words deleted
        public void resetEncryptWord()
        {
            status = false;

            word = String.Empty;

            encryptedWord = String.Empty;
        }

        /// <summary>
        /// Used to compare against guesses
        /// </summary>
        /// <returns>The shift number.</returns>
        /// precondition: none
        /// postcondition: none
        public int getShiftNum()
        {
            return shiftNum;
        }
    }

    /// <summary>
    /// This class uses the EncryptWord class and introduces a guessing game
    /// aspect. The class keeps track of the number of high, low,
    /// and total guesses, and will output these as well as avg guess value.
    /// Valid input is integers for guessing.
    /// Assumptions: Some of the encryptWord methods will be accessed
    /// throught the encryptWord object instead of having GuessingGame
    /// methods call them.
    /// stored. 
    /// </summary>
    class GuessingGame
    {
        private int numGuesses;

        private int numHighGuesses;

        private int numLowGuesses;

        private int guessTotal;

        public EncryptWord encryptWord; //made public to access methods

        public GuessingGame()
        {
            numGuesses = 0;

            numHighGuesses = 0;

            numLowGuesses = 0;

            guessTotal = 0; //used to calculate average of guesses

            encryptWord = new EncryptWord();

        }

        /// <summary>
        /// Lets the user guess the shift number and lets them know if it is
        /// too high, too low, or correct.
        /// </summary>
        /// Depends on EncryptWord class for encrypting the word and some reset
        /// functionality.
        /// <returns>The guess.</returns>
        /// <param name="guess">Guess.</param>
        /// precondition: none
        /// postcondition: numGuesses will increase by 1
        public string checkGuess(int guess)
        {
            int shiftNum = encryptWord.getShiftNum(); //to compare against shift num

            string result = String.Empty; //to let the user if guess is correct

            if (guess > shiftNum)
            {
                numHighGuesses++; //to keep track of high guesses

                result = "too high";

            }
            else if (guess < shiftNum)
            {
                numLowGuesses++; //to keep track of low guesses

                result = "too low";
            }
            else
            {
                result = "correct!";
            }

            numGuesses++; //to keep track of total guesses

            guessTotal += guess; //to calculate average of guesses

            return result;
        }

        /// <summary>
        /// Displays guessing statistics
        /// </summary>
        /// precondition: none
        /// postcondition: none
        public void getStats()
        {
            double avgGuess = 0; //for displaying guess average

            avgGuess = guessTotal / numGuesses;

            Console.WriteLine("Number of guesses: " + numGuesses);

            Console.WriteLine("Number of high guesses: " + numHighGuesses);

            Console.WriteLine("Number of low guesses: " + numLowGuesses);

            Console.WriteLine("Average of guesses: " + avgGuess);

        }

        /// <summary>
        /// Resets the game by clearing the stored words and setting all
        /// guess values to zero.
        /// </summary>
        /// precondition: none;
        /// postcondition: status will be false, all guess values zero, and
        /// words cleared.
        public void resetGame()
        {

            int clearValue = 0; //use to reset guess values

            encryptWord.resetEncryptWord();

            numGuesses = clearValue;

            numHighGuesses = clearValue;

            numLowGuesses = clearValue;

            guessTotal = clearValue;

        }
    }
}