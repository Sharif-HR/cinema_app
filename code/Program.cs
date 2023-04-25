using System;
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

            DotEnv.Load(dotenv);

            Console.Write(Environment.GetEnvironmentVariable("APP_NAME"));
            return;
            Console.Clear();
            Views.Menu MenuPage = new();
            MenuPage.Render();
        }
    }
}
