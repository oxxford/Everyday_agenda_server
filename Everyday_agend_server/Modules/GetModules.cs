using System;
using System.IO;
using Nancy;


namespace Everyday_agend_server.Modules
{
    public class GetModules : NancyModule
    {
        public GetModules()
        {
            //this.RequiresAuthentication();
            /*
             * Gets image from its id and user token
             */
            Get["/getimage/token={token}&imageid={imageid}"] = parameters =>
            {
                int userid = AuthenticationHelper.GetUserFromApiKey(parameters.token).Id;

                return createFileResponse("image/png", userid, parameters.imageid, ".png");
            };

            /*
             * Gets video from its id and user token
             */
            Get["/getvideo/token={token}&videoid={videoid}"] = parameters =>
            {
                int userid = AuthenticationHelper.GetUserFromApiKey(parameters.token).Id;

                return createFileResponse("video/mp4", userid, parameters.videoid, ".mp4");
            };
        }

        /*
         * Returns response with included file and headers
         */
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