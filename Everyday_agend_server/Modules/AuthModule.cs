using System;
using System.IO;
using Everyday_agend_server.JsonModels;
using Nancy;
using Newtonsoft.Json;

namespace Everyday_agend_server.Modules
{
    public class AuthModule : NancyModule
    {
        public AuthModule() : base("/auth")
        {
            /*
             * user's sing in
             */
            Get["/signin/login={login}&password={password}"] = parameters =>
            {
                var apiKey = AuthenticationHelper.ValidateUser(parameters.login, parameters.password);

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

            /*
             * user's sing out
             */
            Delete["/quit"] = args =>
            {
                var apiKey = Request.Headers.Authorization;
                AuthenticationHelper.RemoveApiKey(apiKey);
                return new Response { StatusCode = HttpStatusCode.OK };
            };

            /*
             * user's sing up
             */
            Get["/signup/login={login}&password={password}"] = parameters =>
            {
                try
                {
                    String login = parameters.login;
                    String password = parameters.password;

                    Console.WriteLine(login + password);

                    DatabaseAdapter.createUserEntry(login, password);

                    int userid = DatabaseAdapter.getUserId(login, password);

                    Directory.CreateDirectory("C:\\Users\\g.dzesov\\server\\" + userid);

                    var apiKey = AuthenticationHelper.ValidateUser(login, password);

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
