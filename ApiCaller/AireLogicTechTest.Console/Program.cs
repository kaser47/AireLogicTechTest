using ProgramRunner;

namespace AireLogicTechTest.Console
{
    class Program
    {
        private Runner runner;
        
        static void Main(string[] args)
        {
            Program p = new Program();
            p.setup();
            p.print("Please enter an artist name:");
            string artistName = System.Console.ReadLine();
            double result = p.runner.FindAverageNumberOfWordsByArtistName(artistName);
            p.print($"Result: {result}");
        }

        private void setup()
        {
            runner = new Runner();
            runner.MessageLogged += RunnerOnMessageLogged;
        }

        private void RunnerOnMessageLogged(object sender, MessageEventArgs e)
        {
            print(e.Message);
        }

        private void print(string message)
        {
            System.Console.WriteLine(message);
        }
    }
}