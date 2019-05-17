namespace Hangman.Core
{
    using System;
    using System.Collections.Generic;

    class HangmanGame
    {
        private const int initialGuessCounter = 5;
        private const char MaskSymbol = '*';
        private const string WhitespaceSymbol = " ";
        private const string InputMessage = "Enter a letter:";
        private const string WelcomeMessage = "Welcome to “Hangman” game!\nPlease try to guess my secret word.\n";
        private const string CategoryMessage = "Category: {0}\n";
        private const string ValidationMessage = "Please enter a single letter.";
        private const string GuessedLetterMessage = "You've already tried the letter: {0}";
        private const string WinMessage = "Congratulations, you won! It was {0}.";
        private const string LoseMessage = "You Lost! The correct answer was {0}.";
        private const string IncorrectGuessesMessage = "Incorrect guesses: ";

        private int guessCounter;
        private bool allowInput;

        private string category;
        private readonly string wordToGuess;
        private readonly char[] hiddenWord;

        private readonly List<char> correctGuesses;
        private readonly List<char> incorrectGuesses;
        private readonly GraphicDrawer graphicDrawer;

        public HangmanGame(string wordToGuess)
        {
            this.guessCounter = initialGuessCounter;
            this.allowInput = true;

            this.wordToGuess = wordToGuess;
            this.hiddenWord = this.HideWord();

            this.correctGuesses = new List<char>();
            this.incorrectGuesses = new List<char>();
            this.graphicDrawer = new GraphicDrawer();
        }

        public void Run(string category)
        {
            this.category = category;

            while (this.allowInput)
            {
                this.DisplayIncorrectGuesses();
                this.DisplayGraphic();
                this.ReadInput();
            }
        }

        private char[] HideWord()
        {
            var hiddenWord = new char[this.wordToGuess.Length];

            int lastIndex = wordToGuess.Length - 1;

            hiddenWord[0] = wordToGuess[0];
            hiddenWord[lastIndex] = wordToGuess[lastIndex];

            for (var i = 1; i < this.wordToGuess.Length - 1; i++)
            {
                if (wordToGuess[i] == hiddenWord[0])
                {
                    hiddenWord[i] = hiddenWord[0];
                    continue;
                }

                if (wordToGuess[i] == hiddenWord[lastIndex])
                {
                    hiddenWord[i] = hiddenWord[lastIndex];
                    continue;
                }

                hiddenWord[i] = MaskSymbol;
            }

            return hiddenWord;
        }

        private void DisplayIncorrectGuesses()
        {
            if (!this.allowInput)
            {
                return;
            }

            if (this.incorrectGuesses.Count == 0)
            {
                return;
            }

            var incorrectGuesses = string.Join(WhitespaceSymbol, this.incorrectGuesses.ToArray());

            Console.WriteLine(IncorrectGuessesMessage + incorrectGuesses.ToUpper());
        }

        private void DisplayGraphic()
        {
            string hiddenWordGraphic = string.Join(WhitespaceSymbol, this.hiddenWord);
            string gibbetGraphic = this.graphicDrawer.DrawGibbet(guessCounter);

            Console.WriteLine(gibbetGraphic);
            Console.WriteLine(hiddenWordGraphic.ToUpper());
        }

        private void ReadInput()
        {
            Console.WriteLine();
            Console.WriteLine(InputMessage);
            var input = Console.ReadLine().ToLower();

            Console.Clear();
            Console.WriteLine(WelcomeMessage);
            Console.WriteLine(CategoryMessage, category);
            this.ValidateInput(input);
        }

        private void ValidateInput(string input)
        {
            if (!IsValid(input))
            {
                return;
            }

            if (this.IsLetterGuessed(input))
            {
                return;
            }

            this.CheckInputForMatchInHiddenWord(input);
        }

        private static bool IsValid(string input)
        {
            if (input.Length == 1 && char.IsLetter(input[0]))
            {
                return true;
            }

            Console.WriteLine(ValidationMessage);
            return false;
        }

        private bool IsLetterGuessed(string input)
        {
            bool IsCorrectGuess = this.correctGuesses.Contains(input[0]);
            bool IsIncorrectGuess = this.incorrectGuesses.Contains(input[0]);

            if (!IsCorrectGuess && !IsIncorrectGuess)
            {
                return false;
            }

            Console.WriteLine(string.Format(GuessedLetterMessage, input.ToUpper()));
            return true;
        }

        private void CheckInputForMatchInHiddenWord(string input)
        {
            if (this.wordToGuess.Contains(input))
            {
                this.correctGuesses.Add(input[0]);

                this.ReplaceHiddenWord(input);
                this.CheckForWin();

                return;
            }

            this.incorrectGuesses.Add(input[0]);
            this.guessCounter--;

            this.ReplaceHiddenWord(input);
            this.CheckForLose();
        }

        private void ReplaceHiddenWord(string input)
        {
            for (var i = 0; i < this.wordToGuess.Length; i++)
            {
                this.ReplaceHiddenLetter(this.wordToGuess[i], input, i);
            }
        }

        private void ReplaceHiddenLetter(char letter, string input, int i)
        {
            if (letter != input[0])
            {
                return;
            }

            this.hiddenWord[i] = input[0];
        }

        private void CheckForWin()
        {
            var hiddenWord = string.Join(string.Empty, this.hiddenWord);
            if (hiddenWord != this.wordToGuess)
            {
                return;
            }

            this.GameOver(WinMessage);
        }

        private void CheckForLose()
        {
            if (this.guessCounter != 0)
            {
                return;
            }

            this.GameOver(LoseMessage);
        }

        private void GameOver(string message)
        {
            this.DisplayGraphic();
            Console.WriteLine();
            Console.WriteLine(string.Format(message, this.wordToGuess.ToUpper()));
            this.allowInput = false;
        }
    }
}
