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
        public HelloModule() : base("")
        {
            Get["/"] = parameters =>
            {
                //return Response.AsFile(@"C:\7\image112018.png","image/png");
                
                String fileName = "C:\\7\\lol.mp4";
               
                var c = new FileStream(fileName, FileMode.Open);

                byte[] buffer = new byte[c.Length];

                var length = c.Read(buffer, 0,(int) c.Length);
            
                var response = new Response
                {
                    StatusCode = HttpStatusCode.OK,
                    ContentType = "video/mp4",
                    Contents = stream =>
                    {
                        try
                        {
                            stream.Write(buffer, 0, buffer.Length);
                        }
                        catch (Exception) { }
                    }
                };

                c.Close();

                return response;
                /*var response = new Response
                {
                    Headers =
                    {
                        ["Content-Type"] = "video/mp4",
                        ["Accept-Ranges"] = "bytes",
                        ["Connection"] = "keep-alive",
                        ["Date"] = DateTime.Today.ToString(),
                        ["Etag"] = "1468800"
                    },

                    Contents = s =>
                    {
                        String fileName = "C:\\7\\video.mp4";
                        using (var stream = new FileStream(fileName, FileMode.Open))
                            stream.CopyTo(s);
                        s.Flush();
                        s.Close();
                    }
                };

                Console.WriteLine(response.Headers.ContainsKey("Connection"));

                return response;*/
                //var stream = new FileStream(@"C:\7\video112018.mp4", FileMode.Open);

                //return Response.FromStream(stream, "video/mp4").WithHeader("Connection", "keep-alive");
            };
        }
    }
}
