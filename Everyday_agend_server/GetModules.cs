using System;
using System.IO;
using Nancy;

namespace Everyday_agend_server
{
    class GetModules : NancyModule
    {
        public GetModules()
        {
            Get["/getimage/userid={userid}&imageid={imageid}"] = parameters =>
            {
                //TODO verification

                return createFileResponse("image/png", parameters.userid, parameters.imageid);
            };

            Get["/getvideo/userid={userid}&videoid={videoid}"] = parameters =>
            {
                //TODO verification

                return createFileResponse("video/mp4", parameters.userid, parameters.videoid);
            };
        }

        private Response createFileResponse(String contentType, String userId, String itemId)
        {
            return new Response
            {
                ContentType = contentType,

                Contents = s =>
                {
                    String fileName = "\\" + userId + "\\" + itemId;
                    using (var stream = new FileStream(fileName, FileMode.Open))
                        stream.CopyTo(s);
                }
            };
        }
    }
}
