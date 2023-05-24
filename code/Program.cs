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
            // initialize empty storage
            LocalStorage.WriteToStorage();
            // Load the local storage
            LocalStorage.LoadLocalStorage();

            Views.RouteHandeler.View("MenuPage");
        }
    }
}
