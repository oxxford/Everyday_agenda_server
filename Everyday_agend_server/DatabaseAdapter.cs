using System;


namespace Everyday_agend_server
{
    static class DatabaseAdapter
    {
        static DatabaseAdapter()
        {
            //TODO
            //intialize database
        }

        public static int getImageId(DateTime date, int userId)
        {
            //TODO
            return 0;
        }

        public static int getVideoId(DateTime date, int userId)
        {
            //TODO
            return 0;
        }

        public static String getText(DateTime date, int userId)
        {
            //TODO
            return null;
        }

        public static int getUserId(String username, String password)
        {
            //TODO
            return 0;
        }

        public static void storeImageId(DateTime date, int userId, int imageId)
        {
            //TODO
        }

        public static void storeVideoId(DateTime date, int userId, int videoId)
        {
            //TODO
        }

        public static void storeText(DateTime date, int userId, String text)
        {
            //TODO
        }

        public static void createUserEntry(String username, String password)
        {
            //TODO
        }

        public static bool isImpressionPresent(DateTime date, int userId)
        {
            //TODO
            return false;
        }

        public static bool isValidUserId(int userId)
        {
            return false;
        }
    }
}
