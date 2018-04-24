﻿using System.Collections.Generic;
using Nancy.Security;

namespace Everyday_agend_server
{
    public class Identity : IUserIdentity
    {
        public Identity(int id)
        {
            Id = id;
            UserName = id.ToString();
        }

        public int Id { get; set; }
        public string UserName { get; }
        public IEnumerable<string> Claims { get; }
    }
}
