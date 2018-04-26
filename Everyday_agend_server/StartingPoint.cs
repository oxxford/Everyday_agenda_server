using System;
using Nancy;
using Nancy.Hosting.Self;


namespace Everyday_agend_server
{
    class StartingPoint
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

    /*
     * Simple module used only for introduction
     */
    public class HelloModule : NancyModule
    {
        public HelloModule() : base("")
        {
            Get["/"] = parameters => { return "Hello from Everyday Agenda!"; };
        }
    }
}
