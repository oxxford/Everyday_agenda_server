using System;
using System.Linq;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace Everyday_agend_server
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        //protected override DiagnosticsConfiguration DiagnosticsConfiguration
        //    => new DiagnosticsConfiguration {Password = @"Innopolis"};


        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            pipelines.BeforeRequest += ctx =>
            {
                if (!ctx.Request.Headers.Accept.Any())
                    ctx.Request.Headers.Accept = new[] { Tuple.Create("application/json", 1m) };

                if (string.IsNullOrEmpty(ctx.Request.Headers.ContentType))
                    ctx.Request.Headers.ContentType = "application/json";

                return null;
            };


            //StaticConfiguration.EnableRequestTracing = true;
            //StaticConfiguration.DisableErrorTraces = false;

            var configuration =
                new StatelessAuthenticationConfiguration(ctx =>
                {
                    var jwtToken = ctx.Request.Headers.Authorization;

                    return AuthorizationHelper.GetUserFromApiKey(jwtToken);
                    
                });

            AllowAccessToConsumingSite(pipelines);

            StatelessAuthentication.Enable(pipelines, configuration);
        }

        static void AllowAccessToConsumingSite(IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(x =>
            {
                x.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                x.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,DELETE,PUT,OPTIONS");
            });
        }
    }
}