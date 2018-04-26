using System;
using System.IO;
using System.Linq.Expressions;
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
            String fileName = "C:\\Users\\g.dzesov\\server\\" + userId + "\\" + itemId + type;

            var c = new FileStream(fileName, FileMode.Open);

            byte[] buffer = new byte[c.Length];

            var length = c.Read(buffer, 0, (int)c.Length);

            var response = new Response
            {
                StatusCode = HttpStatusCode.OK,
                ContentType = contentType,
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

        }
    }
}