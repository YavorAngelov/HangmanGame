namespace Hangman.Core
{
    using System;

    static class GraphicDrawer
    {
        private const string WrongGuess1 = "|        |    \n";
        private const string WrongGuess2 = "|       /|    \n";
        private const string WrongGuess3 = "|       /|\\  \n";
        private const string WrongGuess4 = "|       /     \n";
        private const string WrongGuess5 = "|       / \\  \n";
        private const string WelcomeMessage = "Welcome to “Hangman” game!\nPlease try to guess my secret word.\n";
        //private const string CategoryMessage = "Category: {0}";

        public static string DrawGibbet(int guessCounter, string category)
        {
            var imageDisplay = new string[8]
            {
               $"Category: {category}\n",
                "__________    \n",
               $"|/       |    Lives: {guessCounter}\n",
                "|        @    \n",
                "|             \n",
                "|             \n",
                "|             \n",
                "|             \n"
            };

            switch (guessCounter)
            {
                case 4:
                    imageDisplay[4] = WrongGuess1;
                    break;
                case 3:
                    imageDisplay[4] = WrongGuess2;
                    break;
                case 2:
                    imageDisplay[4] = WrongGuess3;
                    break;
                case 1:
                    imageDisplay[4] = WrongGuess3;
                    imageDisplay[5] = WrongGuess4;
                    break;
                case 0:
                    imageDisplay[4] = WrongGuess3;
                    imageDisplay[5] = WrongGuess5;
                    break;
            }

            return string.Join(string.Empty, imageDisplay);
        }

    }
}
