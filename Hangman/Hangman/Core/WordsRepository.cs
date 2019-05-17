namespace Hangman.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    class WordsRepository
    {
        private const string CapitalsFile = "../../Files/Capitals.txt";
        private const string CountriesFile = "../../Files/Countries.txt";
        private const string ContinentsFile = "../../Files/Continents.txt";
        private const string ErrorMessage = "Unable to load words.";

        public string GetWord(string categoryName)
        {
            var words = LoadWords(categoryName);
            var rnd = new Random();

            int rndPosition = rnd.Next(words.Count);
            string rndWord = words.ElementAt(rndPosition);

            return rndWord;
        }

        private static List<string> LoadWords(string categoryName)
        {
            string sourceFile = string.Empty;
            var words = new List<string>();

            switch(categoryName)
            {
                case "capital": sourceFile = CapitalsFile;
                    break;
                case "country": sourceFile = CountriesFile;
                    break;
                case "continent": sourceFile = ContinentsFile;
                    break;
            }

            try
            {
                using (var streamReader = new StreamReader(sourceFile))
                {
                    words = StreamWords(streamReader);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(ErrorMessage);
                Console.WriteLine(e.Message);
            }

            return words;
        }

        private static List<string> StreamWords(StreamReader textReader)
        {
            var words = new List<string>();
            bool nextWord = true;

            while (nextWord)
            {
                string currentLine = textReader.ReadLine();

                if (currentLine == null || !currentLine.Any())
                {
                    nextWord = false;
                }
                else
                {
                    words.Add(currentLine);
                }
            }

            return words;
        }
    }
}
