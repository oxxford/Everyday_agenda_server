using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Routing.Constraints;

namespace Everyday_agend_server
{
    class StoreModules : NancyModule
    {
        public StoreModules()
        {
            Post["/saveimage/userid={userid}&date={day}.{month}.{year}"] = parameters =>
            {
                if (DatabaseAdapter.isValidUserId(parameters.userid))
                {
                    Context.Response.StatusCode = HttpStatusCode.BadRequest;
                    Context.Response.ReasonPhrase = "Invalid user id";
                    return null;
                }

                return null;
            };
        }
        
    }
}
