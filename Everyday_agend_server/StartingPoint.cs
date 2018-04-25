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

            
                var response = new Response
                {
                    ContentType = "video/mp4",

                    Contents = s =>
                    {
                        String fileName = @"C:\7\video112018.mp4";
                        using (var stream = new FileStream(fileName, FileMode.Open))
                            stream.CopyTo(s);
                        s.Flush();
                        s.Close();
                    }
                };

                response.Headers.Add("Connection", "keep-alive");

                long fileSize = new FileInfo(@"C:\7\video112018.mp4").Length;
                response.Headers.Add("Content-length", fileSize.ToString());

                return response;
            };
        }
    }
}
