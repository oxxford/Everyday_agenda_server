using System;
using System.IO;
using Nancy;
using Nancy.Security;

namespace Everyday_agend_server
{
    public class GetModules : NancyModule
    {
        public GetModules()
        {
            this.RequiresAuthentication();

            Get["/getimage/imageid={imageid}"] = parameters =>
            {
                //TODO verification
                int userid = AuthorizationHelper.GetUserFromApiKey(Request.Headers.Authorization).Id;

                return createFileResponse("image/png", userid, parameters.imageid, ".png");
            };

            Get["/getvideo/videoid={videoid}"] = parameters =>
            {
                //TODO verification
                int userid = AuthorizationHelper.GetUserFromApiKey(Request.Headers.Authorization).Id;

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
                    String fileName = "\\" + userId + "\\" + itemId + type;
                    using (var stream = new FileStream(fileName, FileMode.Open))
                        stream.CopyTo(s);
                    s.Flush();
                    s.Close();
                }
            };
        }
    }
}
