using System;
using Everyday_agend_server.JsonModels;
using Nancy;
using Nancy.Security;
using Newtonsoft.Json;

namespace Everyday_agend_server.Modules
{
    public class ImpressionModule : NancyModule
    {
        public ImpressionModule()
        {
            this.RequiresAuthentication();

            /*
             * Returns an impression for specified date. Requires authentication
             */
            Get["/impression/date={year}-{month}-{day}"] = parameters =>
            {
                int userid = AuthenticationHelper.GetUserFromApiKey(Request.Headers.Authorization).Id;

                DateTime date = new DateTime(parameters.year, parameters.month, parameters.day);

                String imageid = DatabaseAdapter.getImageId(date, userid);
                String videoid = DatabaseAdapter.getVideoId(date, userid);
                String text = DatabaseAdapter.getText(date, userid);

                bool exists = !(imageid.Equals("-1") && videoid.Equals("") && text.Equals(""));

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
