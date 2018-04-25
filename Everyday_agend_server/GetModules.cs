using System;
using System.IO;
using Nancy;


namespace Everyday_agend_server
{
    public class GetModules : NancyModule
    {
        public GetModules()
        {
            //this.RequiresAuthentication();

            Get["/getimage/token={token}&imageid={imageid}"] = parameters =>
            {
                //TODO verification
                int userid = AuthorizationHelper.GetUserFromApiKey(parameters.token).Id;

                return createFileResponse("image/png", userid, parameters.imageid, ".png");
            };

            Get["/getvideo/token={token}&videoid={videoid}"] = parameters =>
            {
                //TODO verification
                int userid = AuthorizationHelper.GetUserFromApiKey(parameters.token).Id;

                return createFileResponse("video/mp4", userid, parameters.videoid, ".mp4");
            };
        }

        private Response createFileResponse(String contentType, int userId, String itemId, String type)
        {

            return new Response
            {
                ContentType = contentType,

                Contents = s =>
                {
                    String fileName = "C:\\Users\\g.dzesov\\server\\" + userId + "\\" + itemId + type;
                    using (var stream = new FileStream(fileName, FileMode.Open))
                        stream.CopyTo(s);
                    s.Flush();
                    s.Close();
                }
            };

        }
    }
}