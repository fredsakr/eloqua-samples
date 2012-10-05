using CustomObjectSample.Models;
using CustomObjectSample.Models.Assets.CustomObjects;
using RestSharp;

namespace CustomObjectSample
{
    public class CustomObjectHelper
    {
        #region properties

        private readonly RestClient _client;

        #endregion

        #region constructors

        public CustomObjectHelper(string site, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                          {
                              Authenticator = new HttpBasicAuthenticator(site + "\\" + user, password)
                          };
        }

        #endregion

        #region methods

        public CustomObject GetCustomObject(int id)
        {
            var request = new RestRequest(Method.GET)
                              {
                                  RequestFormat = DataFormat.Json,
                                  Resource = "/assets/customObject/" + id
                              };

            var response = _client.Execute<CustomObject>(request);
            return response.Data;
        }

        public SearchResponse<CustomObject> SearchCustomObjects(string searchTerm, int page, int pageSize)
        {
            var request = new RestRequest(Method.GET)
                              {
                                  RequestFormat = DataFormat.Json,
                                  Resource =
                                      string.Format(
                                          "/assets/customObjects?depth=complete&search={0}&page={1}&count={2}",
                                          searchTerm, page, pageSize)
                              };

            var response = _client.Execute<SearchResponse<CustomObject>>(request);

            return response.Data;
        }

        public CustomObject CreateCustomObject(CustomObject customObject)
        {
            var request = new RestRequest(Method.POST)
                              {
                                  RequestFormat = DataFormat.Json,
                                  Resource = "/assets/customObject"
                              };
            request.AddBody(customObject);

            var response = _client.Execute<CustomObject>(request);

            return response.Data;
        }

        public CustomObject UpdateCustomObject(CustomObject customObject)
        {
            var request = new RestRequest(Method.PUT)
                              {
                                  RequestFormat = DataFormat.Json,
                                  Resource = "/assets/customObject/" + customObject.id
                              };
            request.AddBody(customObject);

            var response = _client.Execute<CustomObject>(request);

            return response.Data;
        }

        public ResponseStatus DeleteCustomObject(int id)
        {
            var request = new RestRequest(Method.DELETE)
                              {
                                  RequestFormat = DataFormat.Json,
                                  Resource = "/assets/customObject/" + id
                              };

            var response = _client.Execute(request);

            return response.ResponseStatus;
        }

        #endregion
    }
}