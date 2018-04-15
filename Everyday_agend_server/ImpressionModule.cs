using System;
using Nancy;
using Newtonsoft.Json;

namespace Everyday_agend_server
{
    public class ImpressionModule : NancyModule
    {
        public ImpressionModule()
        {
            Get["/impression/userid={userid}&date={year}-{month}-{day}"] = parameters =>
            {
                Console.WriteLine("lol");
                DateTime date = new DateTime(parameters.year, parameters.month, parameters.day);

                String imageid = DatabaseAdapter.getImageId(date, parameters.userid);
                String videoid = DatabaseAdapter.getVideoId(date, parameters.userid);
                String text = DatabaseAdapter.getText(date, parameters.userid);

                JsonImpressionModel model = new JsonImpressionModel
                {
                    imageid = imageid,
                    videoid = videoid,
                    text = text
                };

                String json = JsonConvert.SerializeObject(model);

                var response = (Response) json;

                response.ContentType = "application/json";

                return response;
            };
        }
    }
}
