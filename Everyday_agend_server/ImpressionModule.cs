using System;
using Nancy;
using Nancy.Security;
using Newtonsoft.Json;

namespace Everyday_agend_server
{
    public class ImpressionModule : NancyModule
    {
        public ImpressionModule()
        {
            this.RequiresAuthentication();

            Get["/impression/date={year}-{month}-{day}"] = parameters =>
            {
                int userid = AuthorizationHelper.GetUserFromApiKey(Request.Headers.Authorization).Id;

                DateTime date = new DateTime(parameters.year, parameters.month, parameters.day);

                String imageid = DatabaseAdapter.getImageId(date, userid);
                String videoid = DatabaseAdapter.getVideoId(date, userid);
                String text = DatabaseAdapter.getText(date, userid);

                bool exists = !(imageid.Equals("") && videoid.Equals("") && text.Equals(""));

                JsonImpressionModel model = new JsonImpressionModel
                {
                    Exists = exists,
                    Imageid = imageid,
                    Videoid = videoid,
                    Text = text
                };

                String json = JsonConvert.SerializeObject(model);

                var response = (Response) json;

                response.ContentType = "application/json";

                return response;
            };
        }
    }
}
