using CustomObjectDataSample.Models;
using RestSharp;

namespace CustomObjectDataSample
{
    public class CustomObjectFieldHelper
    {
        private readonly RestClient _client;

        public CustomObjectFieldHelper(string site, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                {
                    Authenticator = new HttpBasicAuthenticator(site + "\\" + user, password)
                };
        }

        public SearchResponse<Field> GetCustomObjectFields(int customObjectId)
        {
            var request = new RestRequest(Method.GET)
                {
                    Resource = string.Format("/customObject/{0}/fields?", customObjectId)
                };

            var response = _client.Execute<SearchResponse<Field>>(request);
            return response.Data;
        }
    }
}
