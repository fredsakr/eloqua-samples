using System.Collections.Generic;
using RestSharp;
using ContactSample.Models;

namespace ContactSample
{
    public class ContactHelper
    {
        #region properties

        private readonly RestClient _client;

        #endregion

        #region constructors

        public ContactHelper(string instance, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl) { Authenticator = new HttpBasicAuthenticator(instance + "\\" + user, password) };
        }

        #endregion

        #region contact helpers

        public List<Contact> SearchContacts(string searchTerm, int page, int pageSize)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          RequestFormat = DataFormat.Json,
                                          Resource = string.Format("/data/contacts?search={0}&page={1}&count={2}&depth=complete", searchTerm,
                                                            page, pageSize)
                                      };

            IRestResponse<RequestObjectList<Contact>> response = _client.Execute<RequestObjectList<Contact>>(request);

            List<Contact> contacts = response.Data.elements;

            return contacts;
        }

        #endregion
    }
}
