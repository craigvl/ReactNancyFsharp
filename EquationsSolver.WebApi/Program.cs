using System;
using System.Configuration;
using Microsoft.Owin.Hosting;

namespace EquationsSolver.WebApi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Starting Web Server... ");
            var url = ConfigurationManager.AppSettings.Get("owin:url");
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Running on {0}", url);
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }
    }
}