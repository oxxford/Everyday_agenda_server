using System;
using System.Globalization;
using MySql.Data.MySqlClient;


namespace Everyday_agend_server
{
    static class DatabaseAdapter
    {
        private static string server = "server=dzesov.me;";
        private static string user = "user=dima;";
        private static string database = "database=everyday_agenda;";
        private static string password = "password=verySecure;";
        private static string connectionString = server + user + database + password;
      
        /*
         * Method returns the imageId for a specific user's given its Id and Date stamp.
         * If the image was not found it returns -1
         */
        public static string getImageId(DateTime date, int userId)
        {
            string result = getMultimedia(date, userId, "image_id");
            if (result.Equals(""))
                return "-1";
            else
                return result;
        }

        /*
         * Method returns the videoId for a specific user's given its Id and Date stamp.
         * If the video was not found it returns -1
         */
        public static string getVideoId(DateTime date, int userId)
        {
            string result = getMultimedia(date, userId, "video_id");
            if (result.Equals(""))
                return "";
            else
                return result;
        }

        /*
         * Method returns the text entry for a specific user given user's Id and Date stamp.
         * If the text was not found it returns the empty string.
         */
        public static string getText(DateTime date, int userId)
        {
            return getMultimedia(date, userId, "text");
        }

        /*
         * This method returns a user's id given its email and password.
         * If user is not found or the password is incorrect, the method returns -1
         */
        public static int getUserId(String email, String password)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM UserAccounts WHERE email = '" + email +
                    "' AND password = '" + password + "';";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader()) {
                    string result = "";
                    if (reader.Read())
                    {
                    result = reader[0].ToString();
                    }

                    if (result.Equals(""))
                        return -1;
                    else
                        return int.Parse(result);
                }
            }
        }

        /*
         * This method is used to store image id in the database
         */
        public static void storeImageId(DateTime date, int userId, String imageId)
        {
            storeId(date, userId, imageId, "image_id");
        }

        /*
         * This method is used to store video id in the database
         */
        public static void storeVideoId(DateTime date, int userId, String videoId)
        {
            storeId(date, userId, videoId, "video_id");
        }

        /*
         * This method is used to store text in the database
         */
        public static void storeText(DateTime date, int userId, String text)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                int impressionId = getImpressionId(date, userId, true);
                string quoute = "\'";
                string query = "UPDATE UserImpressions SET text = '" + text.Replace(quoute, quoute + "" + quoute) 
                                + "' WHERE " +"id = " + impressionId + ";";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }

        /*
         * This method creates a new user with a given email and a password. If a user 
         * with given email already exists, it throws an exception.
         */
        public static void createUserEntry(String email, String password)
        {
            if (checkIfUserExists(email)) {
                throw new ArgumentException("User with this email already exsits");
            }
            else {
                string quoute = "\'";
                using (MySqlConnection connection = new MySqlConnection(connectionString)) {
                    connection.Open();
                    string query = "INSERT INTO UserAccounts(email, password) VALUES('" +
                        email + "', '" + password.Replace(quoute, quoute + "" + quoute) + "');";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static bool isImpressionPresent(DateTime date, int userId)
        {
            return !(getImpressionId(date, userId, false) == -1);
        }

        /*
         * This method checks whether the given userId is valid or not
         */
        public static bool isValidUserId(int userId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM UserAccounts WHERE id = '" + userId + "';";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader()) 
                {
                    if (reader.Read())
                    {
                        return !reader[0].ToString().Equals("");
                    }
                    else
                        throw new Exception("Database is not responding");
                }

            }
        }

        /*
        * Method responsible for returning media id's (image, video, audio)
        */
        private static string getMultimedia(DateTime date, int userId, String type)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT " + type + " FROM UserImpressions WHERE user_id = " + userId
                    + " AND submission_date LIKE '" + date.ToString("yyyy-MM-dd") + "%';";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    string result = "";
                    if (reader.Read())
                    {
                        result = reader[0].ToString();
                    }
                    return result;
                }
            }
        }

        /*
         * This method is used to store iD of a given media in the database.
         */
        private static void storeId(DateTime date, int userId, String mediaId, String type) {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                int impressionId = getImpressionId(date, userId, true);
                string query = "UPDATE UserImpressions SET " + type + " = '" + mediaId + "' WHERE " +
                    "id = " + impressionId + ";";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
        /*
         * This method is used to create an empty expression for a specific user in a given date.
         */
        private static int createEmptyImpression(DateTime date, int userId) {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO UserImpressions(submission_date, user_id) VALUES ('" +
                    date.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) + "', " +
                        userId + ");";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                query = "SELECT LAST_INSERT_ID()";
                command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return int.Parse(reader[0].ToString());
                    }
                    else
                        throw new Exception("Database is not responding");
                }

            }
        }
        /*
         * This method is used to get the id of impression for a given user and date.
         * createEmpty is a flag that indicates whether you want to create an empty impression
         * if it doesn't exist.
         */
        private static int getImpressionId(DateTime date, int userId, bool createEmpty) {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM UserImpressions WHERE user_id = '" + userId +
                    "' AND submission_date LIKE '" + date.ToString("yyyy-MM-dd") + "%';";
                MySqlCommand command = new MySqlCommand(query, connection);
                string impression_id = "";
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        impression_id = reader[0].ToString();
                    }
                }
                if (impression_id.Equals(""))
                {
                    if (createEmpty)
                        return createEmptyImpression(date, userId);
                    else
                        return -1;
                }
                else
                    return int.Parse(impression_id);
            }
        }

        /*
         * This method checks whether user with a given email exists in in the database.
         */
        private static bool checkIfUserExists(String email) {
            using (MySqlConnection connection = new MySqlConnection(connectionString)) {
                connection.Open();
                string query = "SELECT id FROM UserAccounts WHERE email = '" + email + "';";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader()) 
                {
                    if (reader.Read())
                    {
                        return !reader[0].ToString().Equals("");
                    }
                    return false;
                        
                }
            }
        }

    }
}
