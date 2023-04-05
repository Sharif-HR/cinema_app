using System;

namespace CinemaApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Views.Menu MenuPage = new();
            MenuPage.Render();
        }
    }
}