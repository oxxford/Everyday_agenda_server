using System;
using System.Collections.Generic;
using System.Linq;


namespace Everyday_agend_server
{
    public class AuthenticationHelper
    {
        //A storage of all logged in users. Stores id and ApiKey - unique authentication identifier
        static readonly List<Tuple<int, string>> ActiveApiKeys = new List<Tuple<int, string>>();
        
        /*
         * Get user entity from its authentication identifier
         */
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

        /*
         * Check presence of a user with such username and password. If exists - return its ApiKey
         */
        public static string ValidateUser(string username, string password)
        {
            var userId = DatabaseAdapter.getUserId(username, password);

            if (userId == -1)
            {
                return null;
            }

            var apiKey = Guid.NewGuid().ToString();
            ActiveApiKeys.Add(new Tuple<int, string>(userId, apiKey));
            return apiKey;
        }

        /*
         * When user log out
         */
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

