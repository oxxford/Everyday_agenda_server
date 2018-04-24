using System;
using System.IO;
using System.Security.Principal;
using Nancy;
using Nancy.Authentication.Stateless;
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

    public class HelloModule : NancyModule
    {
        public HelloModule()
        {
            Put["/"] = parameters =>
            {
                using (var fileStream = File.Create("\\lol.png"))
                {
                    Context.Request.Body.CopyTo(fileStream);
                }
                /*int day = Int32.Parse(parameters.day);
                int month = Int32.Parse(parameters.month); 
                int year = Int32.Parse(parameters.year);
                
                DateTime date = new DateTime(year, month, day);
                Console.Write(date);
                JsonStorieModel[] arr = new JsonStorieModel[1];

                arr[0] = new JsonStorieModel
                {
                    date = new DateTime(1, 1, 1),
                    imageid = "lol"
                };

                String json = JsonConvert.SerializeObject(arr);

                var response = (Response)json;
                response.ContentType = "application/json";*/

                return HttpStatusCode.OK;
            };
        }
    }
}
