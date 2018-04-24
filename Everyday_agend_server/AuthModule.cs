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
                var apiKey = AuthorizationHelper.ValidateUser(parameters.login, parameters.password);

                if (apiKey == null)
                    return new Response
                    {
                        StatusCode = HttpStatusCode.Unauthorized
                    };

                JsonUserModel m = new JsonUserModel
                {
                    Token = apiKey
                };

                String json = JsonConvert.SerializeObject(m);

                var response = (Response) json;
                response.ContentType = "application/json";

                return response;
            };

            Delete["/"] = args =>
            {
                var apiKey = Request.Headers.Authorization;
                AuthorizationHelper.RemoveApiKey(apiKey);
                return new Response { StatusCode = HttpStatusCode.OK };
            };
        }
    }
}
