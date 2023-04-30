using System;
using System.Threading;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace CinemaApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var root = Directory.GetCurrentDirectory();
            var dotenv = Path.Combine(root, ".env");

            // Load the env file
            DotEnv.Load(dotenv);
            // Load the local storage
            LocalStorage.LoadLocalStorage();

            // start the go back function so that it is always listening for input
            Thread backgroundThread = new Thread(new ThreadStart(GoBack));
            backgroundThread.Start();

            Views.RouteHandeler.View("MenuPage");
        }

        static void GoBack() {
            ConsoleKeyInfo Key = Console.ReadKey(true);

            if(Key.Key == ConsoleKey.Escape) {
                Console.WriteLine("Directing you back...");
                Thread.Sleep(5000);
                Views.RouteHandeler.LastView();
            }
        }
    }
}
