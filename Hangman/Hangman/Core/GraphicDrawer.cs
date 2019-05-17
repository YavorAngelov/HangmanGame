namespace Hangman.Core
{
    class GraphicDrawer
    {
        private const string WrongGuess1 = "|        @    \n";
        private const string WrongGuess2 = "|        |    \n";
        private const string WrongGuess3 = "|       /|    \n";
        private const string WrongGuess4 = "|       /|\\  \n";
        private const string WrongGuess5 = "|       /     \n";
        private const string WrongGuess6 = "|       / \\  \n";

        public string DrawGibbet(int guessCounter)
        {
            var imageDisplay = new string[7]
            {
                "__________    \n",
                "|/       |    \n",
                "|        |    \n",
                "|        @    \n",
                "|             \n",
                "|             \n",
                "|             \n" 
            };

            switch (guessCounter)
            {
                case 4:
                    imageDisplay[4] = WrongGuess2;
                    break;
                case 3:
                    imageDisplay[4] = WrongGuess3;
                    break;
                case 2:
                    imageDisplay[4] = WrongGuess4;
                    break;
                case 1:
                    imageDisplay[4] = WrongGuess4;
                    imageDisplay[5] = WrongGuess5;
                    break;
                case 0:
                    imageDisplay[4] = WrongGuess4;
                    imageDisplay[5] = WrongGuess6;
                    break;
            }

            return string.Join(string.Empty, imageDisplay);
        }
    }
}
