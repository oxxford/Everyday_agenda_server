using System;
using System.IO;
using Nancy;
using Nancy.Hosting.Self;

namespace Everyday_agend_server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:8080")))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:8080");
                Console.ReadLine();
            }
        }
    }

    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Put["/"] = parameters =>
            {
                /*int day = Int32.Parse(parameters.day);
                int month = Int32.Parse(parameters.month); 
                int year = Int32.Parse(parameters.year);
                
                DateTime date = new DateTime(year, month, day);
                Console.Write(date);*/

                using (var fileStream = File.Create("\\lol.mp4"))
                {
                    Context.Request.Body.CopyTo(fileStream);
                }
                return "Hello from Everyday agenda!";
            };
        }
    }
}
