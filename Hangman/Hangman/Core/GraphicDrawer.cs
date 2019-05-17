namespace Hangman.Core
{
    class GraphicDrawer
    {
        private const string WrongGuess1 = "|        |    \n";
        private const string WrongGuess2 = "|       /|    \n";
        private const string WrongGuess3 = "|       /|\\  \n";
        private const string WrongGuess4 = "|       /     \n";
        private const string WrongGuess5 = "|       / \\  \n";

        public string DrawGibbet(int guessCounter)
        {
            var imageDisplay = new string[7]
            {
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
                    imageDisplay[3] = WrongGuess1;
                    break;
                case 3:
                    imageDisplay[3] = WrongGuess2;
                    break;
                case 2:
                    imageDisplay[3] = WrongGuess3;
                    break;
                case 1:
                    imageDisplay[3] = WrongGuess3;
                    imageDisplay[4] = WrongGuess4;
                    break;
                case 0:
                    imageDisplay[3] = WrongGuess3;
                    imageDisplay[4] = WrongGuess5;
                    break;
            }

            return string.Join(string.Empty, imageDisplay);
        }
    }
}
