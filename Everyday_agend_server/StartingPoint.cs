using System;
using System.IO;
using Nancy;
using Nancy.Hosting.Self;
using Nancy.Responses;


namespace Everyday_agend_server
{
    class StartingPoint
    {
        static void Main(string[] args)
        {
            using (var host = new NancyHost(new Uri("http://localhost:8080")))
            {
                DateTime date = new DateTime(2018, 01, 02);
                Console.WriteLine(date.Year + "-" + date.Month + "-" + date.Day);
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
                //return Response.AsFile(@"C:\7\image112018.png","image/png");

                //GenericFileResponse fileResponse = new GenericFileResponse(@"C:\7\image112018.png");
                //return fileResponse;


                /*var response = new Response
                {
                    Headers = { ["Connection"] = "keep-alive", ["Content-Type"] = "video/mp4" },

                    //ContentType = "video/mp4",

                    Contents = s =>
                    {
                        String fileName = @"C:\7\video112018.mp4";
                        using (var stream = new FileStream(fileName, FileMode.Open))
                            stream.CopyTo(s);
                        s.Flush();
                        s.Close();
                    }
                };

                string l = "";

                Console.WriteLine(response.Headers.TryGetValue("Connection",out l));

                return response;*/
                //var stream = new FileStream(@"C:\7\video112018.mp4", FileMode.Open);

                //return Response.FromStream(stream, "video/mp4").WithHeader("Connection", "keep-alive");
                return ";";
            };
        }
    }
}
