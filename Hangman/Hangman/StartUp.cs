namespace Hangman
{
    using Hangman.Core;

    class StartUp
    {
        static void Main(string[] args)
        {
            var engine = new GameEngine();
            engine.Run();
        }
    }
}
