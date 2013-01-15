using System.Collections.Generic;
using CustomObjectDataSample.Models;
using CustomObjectDataSample.Models.Data;
using RestSharp;

namespace CustomObjectDataSample
{
    public class CustomObjectDataHelper
    {
        private readonly RestClient _client;

        public CustomObjectDataHelper(string site, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                {
                    Authenticator = new HttpBasicAuthenticator(site + "\\" + user, password)
                };
        }

        public SearchResponse<CustomObjectData> SearchCustomObjects(int customObjectId, string searchTerm, int page, int pageSize)
        {
            var request = new RestRequest(Method.GET)
                {
                    RequestFormat = DataFormat.Json,
                    Resource =
                        string.Format("/data/customObject/{0}?search={1}&page={2}&count={3}", customObjectId, searchTerm,
                                      page, pageSize)
                };

            var response = _client.Execute<SearchResponse<CustomObjectData>>(request);

            return response.Data;
        }

        public CustomObjectData CreateCustomObjectData(int customObjectId, CustomObjectData customObjectData)
        {
            var request = new RestRequest(Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    Resource = string.Format("/data/customObject/{0}", customObjectId)
                };

            request.AddBody(customObjectData);

            var response = _client.Execute<CustomObjectData>(request);

            return response.Data;
        }

    }
}
