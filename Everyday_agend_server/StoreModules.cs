using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Routing.Constraints;

namespace Everyday_agend_server
{
    public class StoreModules : NancyModule
    {
        public StoreModules()
        {
            Put["/saveimage/userid={userid}&date={day}.{month}.{year}"] = parameters =>
            {
                if (!DatabaseAdapter.isValidUserId(parameters.userid))
                {
                    var response = new Response
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        ReasonPhrase = "Invalid user id!"
                    };

                    return response;
                }

                //TODO
                return Context.Response;
            };
        }
    }
}
