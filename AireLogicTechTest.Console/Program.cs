using System;
using Common;
using ProgramRunner;

namespace AireLogicTechTest.UI
{
    class Program
    {
        private Runner _runner;
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Setup();
            Print("Please enter an artist name:");
            string artistName;
            do
            {
                artistName = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(artistName)) Print("Artist name cannot be null or whitespace. Try again");
            } while (string.IsNullOrWhiteSpace(artistName));

            int result = p._runner.FindAverageNumberOfWordsByArtistName(artistName);
            if (result != 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Print($"Result: {result} average words per song.");
            }
            
            Print("Press ANY key to exit.");
            Console.ReadKey(true);
        }

        private void Setup()
        {
            _runner = new Runner();
            _runner.MessageLogged += RunnerOnMessageLogged;
        }

        private static void RunnerOnMessageLogged(object sender, MessageEventArgs messageEventArgs)
        {
            Print($"Log: {messageEventArgs.Message}");
        }

        private static void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}