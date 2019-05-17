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

            switch (categoryName)
            {
                case "Capital":
                    sourceFile = CapitalsFile;
                    break;
                case "Country":
                    sourceFile = CountriesFile;
                    break;
                case "Continent":
                    sourceFile = ContinentsFile;
                    break;
            }

            try
            {
                words = File.ReadAllLines(sourceFile).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(ErrorMessage);
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }

            return words;
        }
    }
}
