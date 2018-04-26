using System;
using System.IO;
using System.Linq;
using Everyday_agend_server.JsonModels;
using HttpMultipartParser;
using Nancy;
using Nancy.Security;
using Newtonsoft.Json;

namespace Everyday_agend_server.Modules
{
    public class StoreModules : NancyModule
    {
        public StoreModules()
        {
            this.RequiresAuthentication();

            /*
             * Saves image and its id for specified date. Requires authentication
             */
            Put["/saveimage/date={year}-{month}-{day}"] = parameters =>
            {
                int userid = AuthenticationHelper.GetUserFromApiKey(Request.Headers.Authorization).Id;

                String imageId = "image" + parameters.day + parameters.month + parameters.year;
                DateTime date = new DateTime(parameters.year, parameters.month, parameters.day);

                DatabaseAdapter.storeImageId(date, userid, imageId);

                var parser = new MultipartFormDataParser(Request.Body);
                var file = parser.Files.First();
                using (var fileStream = File.Create("C:\\Users\\g.dzesov\\server\\" + userid + "\\" + imageId + ".png"))
                {
                    file.Data.CopyTo(fileStream);
                }

                return HttpStatusCode.OK;
            };

            /*
             * Saves video and its id for specified date. Requires authentication
             */
            Put["/savevideo/date={year}-{month}-{day}"] = parameters =>
            {
                int userid = AuthenticationHelper.GetUserFromApiKey(Request.Headers.Authorization).Id;

                String videoId = "video" + parameters.day + parameters.month + parameters.year;
                DateTime date = new DateTime(parameters.year, parameters.month, parameters.day);

                DatabaseAdapter.storeVideoId(date, userid, videoId);

                var parser = new MultipartFormDataParser(Request.Body);
                var file = parser.Files.First();
                using (var fileStream = File.Create("C:\\Users\\g.dzesov\\server\\" + userid + "\\" + videoId + ".mp4"))
                {
                    file.Data.CopyTo(fileStream);
                }

                return HttpStatusCode.OK;
            };

            /*
             * Saves text for specified date. Requires authentication
             */
            Put["/savetext/date={year}-{month}-{day}"] = parameters =>
            {
                int userid = AuthenticationHelper.GetUserFromApiKey(Request.Headers.Authorization).Id;

                DateTime date = new DateTime(parameters.year, parameters.month, parameters.day);

                String s = Context.Request.Body.ToString();

                JsonTextModel obj = JsonConvert.DeserializeObject<JsonTextModel>(s);

                DatabaseAdapter.storeText(date, userid, obj.text);
                return 0;
            };
        }
    }
}
