using System;
using System.IO;
using System.Linq;
using HttpMultipartParser;
using Nancy;
using Nancy.Hosting.Self;
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
            //this.RequiresAuthentication();

            Get["/"] = parameters =>
            {
                /*var parser = new MultipartFormDataParser(Request.Body);
                var file = parser.Files.First();
                //string filename = file.FileName;
                //Stream data = file.Data;
                using (var fileStream = File.Create("\\lol.mp4"))
                {
                    file.Data.CopyTo(fileStream);
                }*/
                return Response.AsFile("\\lol");

                //return "Hello!";
            };
        }
    }
}
