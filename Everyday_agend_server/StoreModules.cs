using System;
using System.IO;
using Nancy;
using Newtonsoft.Json;

namespace Everyday_agend_server
{
    public class StoreModules : NancyModule
    {
        public StoreModules()
        {
            Put["/saveimage/userid={userid}&date={year}.{month}.{day}"] = parameters =>
            {
                /*if (!DatabaseAdapter.isValidUserId(parameters.userid))
                {
                    var response = new Response
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        ReasonPhrase = "Invalid user id!"
                    };

                    return response;
                }*/

                int userId = parameters.userid;
                String imageId = "image" + parameters.day + parameters.month + parameters.year;
                DateTime date = new DateTime(parameters.year, parameters.month, parameters.day);

                DatabaseAdapter.storeImageId(date, userId, imageId);

                using (var fileStream = File.Create("\\" + userId + "\\" + imageId + ".png"))
                {
                    Context.Request.Body.CopyTo(fileStream);
                }

                return HttpStatusCode.OK;
            };

            Put["/savevideo/userid={userid}&date={year}.{month}.{day}"] = parameters =>
            {
                int userId = parameters.userid;
                String videoId = "video" + parameters.day + parameters.month + parameters.year;
                DateTime date = new DateTime(parameters.year, parameters.month, parameters.day);

                DatabaseAdapter.storeVideoId(date, userId, videoId);

                using (var fileStream = File.Create("\\" + userId + "\\" + videoId + ".mp4"))
                {
                    Context.Request.Body.CopyTo(fileStream);
                }

                return HttpStatusCode.OK;
            };

            Put["/savetext/userid={userid}&date={year}.{month}.{day}"] = parameters =>
            {
                int userId = parameters.userid;
                DateTime date = new DateTime(parameters.year, parameters.month, parameters.day);

                String s = Context.Request.Body.ToString();

                JsonTextModel obj = JsonConvert.DeserializeObject<JsonTextModel>(s);

                DatabaseAdapter.storeText(date, userId, obj.text);
                return 0;
            };
        }
    }
}
