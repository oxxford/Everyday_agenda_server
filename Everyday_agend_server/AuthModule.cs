using System;
using System.IO;
using Nancy;
using Newtonsoft.Json;

namespace Everyday_agend_server
{
    public class AuthModule : NancyModule
    {
        public AuthModule() : base("/auth")
        {
            Get["/signin/login={login}&password={password}"] = parameters =>
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

            Delete["/quit"] = args =>
            {
                var apiKey = Request.Headers.Authorization;
                AuthorizationHelper.RemoveApiKey(apiKey);
                return new Response { StatusCode = HttpStatusCode.OK };
            };

            Get["/signup/login={login}&password={password}"] = parameters =>
            {
                try
                {
                    int userid = DatabaseAdapter.createUserEntry(parameters.login, parameters.password);

                    Directory.CreateDirectory("\\" + userid);

                    var apiKey = AuthorizationHelper.ValidateUser(parameters.login, parameters.password);

                    JsonUserModel m = new JsonUserModel
                    {
                        Token = apiKey
                    };

                    String json = JsonConvert.SerializeObject(m);

                    var response = (Response)json;
                    response.ContentType = "application/json";

                    return response;
                }
                catch (ArgumentException e)
                {
                    return new Response
                    {
                        StatusCode = HttpStatusCode.NotAcceptable,
                        ReasonPhrase = e.Message
                    };
                }
            };
        }
    }
}
