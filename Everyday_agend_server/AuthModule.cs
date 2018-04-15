using System;
using Nancy;
using Newtonsoft.Json;

namespace Everyday_agend_server
{
    public class AuthModule : NancyModule
    {
        public AuthModule()
        {
            Get["/auth/login={login}&password={password}"] = parameters =>
            {
                int userId = DatabaseAdapter.getUserId(parameters.login, parameters.password);

                if (userId == -1)
                    return new Response
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        ReasonPhrase = "Invalid user id!"
                    };

                JsonUserModel m = new JsonUserModel
                {
                    userId = userId
                };

                String json = JsonConvert.SerializeObject(m);

                var response = (Response) json;
                response.ContentType = "application/json";

                return response;
            };
        }
    }
}
