using System;
using System.IO;
using System.Linq;
using HttpMultipartParser;
using Nancy;
using Nancy.Hosting.Self;
using Nancy.Responses;
using Nancy.Security;

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

    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Get["/"] = parameters =>
            {
                return Response.AsFile(@"C:\7\image112018.png","image/png");
            };
        }
    }
}
