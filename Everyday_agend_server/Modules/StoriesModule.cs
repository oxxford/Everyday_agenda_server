﻿using System;
using Everyday_agend_server.JsonModels;
using Nancy;
using Nancy.Security;
using Newtonsoft.Json;

namespace Everyday_agend_server.Modules
{
    public class StoriesModule : NancyModule
    {
        public StoriesModule()
        {
            this.RequiresAuthentication();

            /*
             * Returns stories for user on specified date
             */
            Get["/getstories/date={year}-{month}-{day}"] = parameters =>
            {
                DateTime date = new DateTime(parameters.year, parameters.month, parameters.day);
                int userid = AuthenticationHelper.GetUserFromApiKey(Request.Headers.Authorization).Id;

                JsonStorieModel[] arr = new JsonStorieModel[6];

                arr[0] = getModelFromDate(date, userid, 1, 0);
                arr[1] = getModelFromDate(date, userid, 3, 0);
                arr[2] = getModelFromDate(date, userid, 6, 0);
                arr[3] = getModelFromDate(date, userid, 0, 1);
                arr[4] = getModelFromDate(date, userid, 0, 2);
                arr[5] = getModelFromDate(date, userid, 0, 3);

                String json = JsonConvert.SerializeObject(arr);

                var response = (Response)json;
                response.ContentType = "application/json";

                return response;
            };
        }

        /*
         * Parse dates and returns models with stories
         */
        private JsonStorieModel getModelFromDate(DateTime date, int userid, int months, int years)
        {
            int month = date.Month <= months ? 12 - months + date.Month : date.Month - months;
            int year = date.Month <= months ? date.Year - years - 1 : date.Year - years;
            
            DateTime newDate = new DateTime(year, month, date.Day);

            return new JsonStorieModel
            {
                date = newDate.Year + "-" + newDate.Month + "-" + newDate.Day,
                imageid = DatabaseAdapter.getImageId(newDate, userid)
            };
        }
    }
}
