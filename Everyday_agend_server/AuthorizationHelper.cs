using System;
using System.Collections.Generic;
using System.Linq;


namespace Everyday_agend_server
{
    class AuthorizationHelper
    {
        static readonly List<Tuple<int, string>> ActiveApiKeys = new List<Tuple<int, string>>();
        //private static readonly List<Tuple<string, string>> Users = new List<Tuple<string, string>>();

        public static Identity GetUserFromApiKey(string apiKey)
        {
            var activeKey = ActiveApiKeys.FirstOrDefault(x => x.Item2 == apiKey);

            if (activeKey == null)
            {
                return null;
            }

            var userId = activeKey.Item1;
            return new Identity(userId);
        }

        public static string ValidateUser(string username, string password)
        {
            //try to get a user from the "database" that matches the given username and password
            var userId = DatabaseAdapter.getUserId(username, password);

            if (userId == -1)
            {
                return null;
            }

            //now that the user is validated, create an api key that can be used for subsequent requests
            var apiKey = Guid.NewGuid().ToString();
            ActiveApiKeys.Add(new Tuple<int, string>(userId, apiKey));
            return apiKey;
        }

        public static void RemoveApiKey(string apiKey)
        {
            var apiKeyToRemove = ActiveApiKeys.First(x => x.Item2 == apiKey);
            ActiveApiKeys.Remove(apiKeyToRemove);
        }

        public static void CreateUser(string username, string password)
        {
            DatabaseAdapter.createUserEntry(username, password);
        }
    }
}

