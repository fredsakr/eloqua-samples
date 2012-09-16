using CustomDataImportSample.Models;
using CustomDataImportSample.Models.CustomObjects;
using RestSharp;

namespace CustomDataImportSample
{
    public class CustomDataHelper
    {
        #region constructors

        public CustomDataHelper(string site, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                          {
                              Authenticator = new HttpBasicAuthenticator(site + "\\" + user, password)
                          };
        }

        #endregion

        #region properties

        private readonly RestClient _client;

        #endregion

        #region methods

        /// <summary>
        /// Returns a list of <see cref="CustomObjectSearchResponse"/>
        /// </summary>
        /// <param name="searchTerm">The name of the custom data object to search for</param>
        /// <param name="page">The page number (see response total)</param>
        /// <param name="pageSize">The number of records to include in each page of results</param>
        public RestObjectList<CustomObjectSearchResponse> SearchCustomDataObjects(string searchTerm, int page, int pageSize)
        {
            var request = new RestSharp.RestRequest(Method.GET)
                              {
                                  Resource =
                                      string.Format("/customObjects?search={0}&page={1}&pageSize={2}", searchTerm,
                                                    page, pageSize)
                              };
            IRestResponse<RestObjectList<CustomObjectSearchResponse>> response = _client.Execute<RestObjectList<CustomObjectSearchResponse>>(request);
            return response.Data;
        }

        /// <summary>
        /// Retrieve a <see cref="CustomObject"/> by its uri
        /// </summary>
        /// <param name="id">The uri of the <see cref="CustomObject"/></param>
        /// <returns></returns>
        public RestObjectList<Field> GetCustomObjectFields(int id)
        {
            var request = new RestRequest(Method.GET)
                              {
                                  Resource = string.Format("/customObject/{0}/fields?", id)
                              };
            IRestResponse<RestObjectList<Field>> response = _client.Execute<RestObjectList<Field>>(request);
            return response.Data;
        }

        #endregion
    }
}
