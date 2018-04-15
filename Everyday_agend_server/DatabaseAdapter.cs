using System;


namespace Everyday_agend_server
{
    class DatabaseAdapter
    {
        public int getImageId(DateTime date, int userId)
        {
            //TODO
            return 0;
        }

        public int getVideoId(DateTime date, int userId)
        {
            //TODO
            return 0;
        }

        public String getText(DateTime date, int userId)
        {
            //TODO
            return null;
        }

        public int getUserId(String username, String password)
        {
            //TODO
            return 0;
        }

        public void storeImageId(DateTime date, int userId, int imageId)
        {
            //TODO
        }

        public void storeVideoId(DateTime date, int userId, int videoId)
        {
            //TODO
        }

        public void storeText(DateTime date, int userId, String text)
        {
            //TODO
        }

        public void createUserEntry(String username, String password)
        {
            //TODO
        }

        public bool isImpressionPresent(DateTime date, int userId)
        {
            //TODO
            return false;
        }
    }
}
