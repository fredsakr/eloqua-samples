using System.Net;
using ContactSample.Models;
using RestSharp;

namespace ContactSample
{
    public class ContactHelper
    {
        #region properties

        private readonly RestSharp.RestClient _client;

        #endregion

        #region constructors

        public ContactHelper(string site, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                          {
                              Authenticator = new HttpBasicAuthenticator(site + "\\" + user, password)
                          };
        }

        #endregion

        #region methods

        public Contact GetContact(int id)
        {
            var request = new RestRequest(Method.GET)
                              {
                                  Resource = "/data/contact/" + id
                              };

            var response = _client.Execute<Contact>(request);
            return response.Data;
        }

        public RequestObjectList<Contact> SearchContacts(string searchTerm, int page, int pageSize)
        {
            var request = new RestRequest(Method.GET)
                              {
                                  Resource =
                                      string.Format("/data/contacts?depth=complete&search={0}&page={1}&count={2}",
                                                    searchTerm, page, pageSize)
                              };

            var response = _client.Execute<RequestObjectList<Contact>>(request);
            return response.Data;
        } 

        public Contact CreateContact(Contact contact)
        {
            var request = new RestRequest(Method.POST)
                              {
                                  Resource = "/data/contact/",
                                  RequestFormat = DataFormat.Json
                              };
            request.AddBody(contact);

            var response = _client.Execute<Contact>(request);
            return response.Data;
        }

        public Contact UpdateContact(Contact contact)
        {
            var request = new RestRequest(Method.PUT)
                              {
                                  Resource = "/data/contact/" + contact.id,
                                  RequestFormat = DataFormat.Json
                              };
            request.AddBody(contact);

            var response = _client.Execute<Contact>(request);
            return response.Data;
        }

        public HttpStatusCode DeleteContact(int id)
        {
            var request = new RestRequest(Method.DELETE)
                              {
                                  Resource = "/data/contact/" + id
                              };
            var response = _client.Execute<Contact>(request);
            return response.StatusCode;
        }


        #endregion
    }
}
