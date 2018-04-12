using System;
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
            Get["/{name}/{id}"] = parameters =>
            {
                Console.Write((String)parameters.id);
                return parameters.name;
            };
        }
    }
}
