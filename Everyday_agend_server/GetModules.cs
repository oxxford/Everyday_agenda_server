using System;
using System.IO;
using Nancy;
using Nancy.Responses;
using Nancy.Security;

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
            var file = new FileStream(fileName, FileMode.Open);

            Console.Write(MimeTypes.GetMimeType(fileName));
            

            var response = new StreamResponse(() => file, MimeTypes.GetMimeType(fileName));

            response.ContentType = contentType;

            file.Close();

            return response;

        }
    }
}