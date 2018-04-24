using System;
using Nancy;
using Newtonsoft.Json;

namespace Everyday_agend_server
{
    public class StoriesModule : NancyModule
    {
        public StoriesModule()
        {
            Get["/getstories/userid={userid}&date={year}-{month}-{day}"] = parameters =>
            {
                DateTime date = new DateTime(parameters.year, parameters.month, parameters.day);
                int userid = parameters.userid;

                JsonStorieModel[] arr = new JsonStorieModel[6];

                arr[0] = getModelFromDate(date, userid, 1, 0);
                arr[1] = getModelFromDate(date, userid, 3, 0);
                arr[2] = getModelFromDate(date, userid, 6, 0);
                arr[3] = getModelFromDate(date, userid, 0, 1);
                arr[4] = getModelFromDate(date, userid, 0, 2);
                arr[5] = getModelFromDate(date, userid, 0, 3);

                /*class c
                {
                    JsonStorieModel arr;
                }*/

                String json = JsonConvert.SerializeObject(arr);

                var response = (Response)json;
                response.ContentType = "application/json";

                return response;
            };
        }

        private JsonStorieModel getModelFromDate(DateTime date, int userid, int months, int years)
        {
            int month = date.Month <= months ? 12 - months + date.Month : date.Month - months;
            int year = date.Month <= months ? date.Year - years - 1 : date.Year - years;

            Console.WriteLine(year);
            Console.WriteLine(month);
            Console.WriteLine(date.Day);

            DateTime newDate = new DateTime(year, month, date.Day);

            return new JsonStorieModel
            {
                date = newDate,
                imageid = DatabaseAdapter.getImageId(newDate, userid)
            };
        }
    }
}
