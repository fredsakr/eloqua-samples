using System;
using System.Collections.Generic;
using ActivitySample.Models;
using RestSharp;

namespace ActivitySample
{
    public class ActivityHelper
    {
        #region properties

        private readonly RestClient _client;

        #endregion

        #region constructors

        public ActivityHelper(string instance, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                          {
                              Authenticator = new HttpBasicAuthenticator(instance + "\\" + user, password)
                          };
        }

        #endregion

        #region Search Operations

        /// <summary>
        /// Invokes an HTTP GET request to retrieve a list of activities for a Contact
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Activity> GetActivities(int contactId, DateTime startDate, DateTime endDate, string type, int? count)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          RequestFormat = DataFormat.Json,
                                          Resource = string.Format("/data/activities/contact/{0}?startDate={1}&endDate={2}&type={3}&count={4}", 
                                                            contactId, ConvertToUnixEpoch(startDate), ConvertToUnixEpoch(endDate), type, count)
                                      };

            IRestResponse<List<Activity>> response = _client.Execute<List<Activity>>(request);
            return response.Data;
        }

        #endregion

        #region Unix time

        private static DateTime _unixEpochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static long ConvertToUnixEpoch(DateTime date)
        {
            return (long)new TimeSpan(date.Ticks - _unixEpochTime.Ticks).TotalSeconds;
        }

        #endregion
    }
}
