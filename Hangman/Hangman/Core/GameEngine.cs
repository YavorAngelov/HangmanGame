namespace Hangman.Core
{
    using System;

    class GameEngine
    {
        private const string WelcomeMessage = "Welcome to “Hangman” game!\nPlease try to guess my secret word.\n";
        private const string SelectCategoryMessage = "Please select category.\n[1/Capitals]\n[2/Countries]\n[3/Continents]";
        private const string CategoryMessage = "Category: {0}";
        private const string WrongCategoryMessage = "Please select a number from 1 to 3.";
        private const string PlayAgainMessage = "Would you like to play again?\n[Y/N]";

        private readonly WordsRepository wordsRepository;

        public GameEngine()
        {
            this.wordsRepository = new WordsRepository();
        }

        public void Run()
        {
            bool play = true;
            while (play)
            {
                DisplayWelcomeMessage();

                string categoryNumber = Console.ReadLine();
                string category = this.ParseCategory(categoryNumber);

                Console.Clear();

                string word = this.wordsRepository.GetWord(category);
                var hangmanGame = new HangmanGame(word);

                hangmanGame.Run(category);

                play = PlayAgain();
            }
        }

        private string ParseCategory(string categoryNumber)
        {
            string category = string.Empty;

            int number = 0;
            bool selected = int.TryParse(categoryNumber, out number);

            if (!selected || number < 0 || number > 3)
            {
                Console.Clear();

                DisplayWelcomeMessage();
                Console.WriteLine(WrongCategoryMessage);

                category = this.ParseCategory(Console.ReadLine());
            }

            switch (number)
            {
                case 1:
                    category = "Capital";
                    break;
                case 2:
                    category = "Country";
                    break;
                case 3:
                    category = "Continent";
                    break;
            }

            return category;
        }

        private static bool PlayAgain()
        {
            Console.WriteLine(PlayAgainMessage);
            var input = Console.ReadKey().Key;

            if (input != ConsoleKey.Y)
            {
                Console.WriteLine();
                Environment.Exit(0);
            }

            Console.Clear();
            return true;
        }

        private static void DisplayWelcomeMessage()
        {
            Console.WriteLine(WelcomeMessage);
            Console.WriteLine(SelectCategoryMessage);
        }
    }
}
