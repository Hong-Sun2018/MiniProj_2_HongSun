using MiniProj_HongSun.Controlers;
using MiniProj_HongSun.Models;
using MiniProj_HongSun.Views;
using System;
using System.Collections.Generic;

namespace MiniProj_HongSun
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            View view = new View();
            view.WriteLineGreen("Fetching online rates...");
            view.RateController.FetchOnlineRates().Wait();
           
            view.DisplayMainMenu();

            Console.WriteLine("Hello World!");
        }
    }
}
