using System;
using System.Collections.Generic;
using ContactSegmentSample.Models;
using RestSharp;

namespace ContactSegmentSample
{
    public class ContactSegmentHelper
    {
        #region

        private readonly RestClient _client;

        #endregion

        #region constructors

        public ContactSegmentHelper(string site, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                          {
                              Authenticator = new HttpBasicAuthenticator(site + "\\" + user, password)
                          };
        }

        #endregion

        #region CRUD operations

        /// <summary>
        /// Invokes an HTTP GET request to retrieve a single <see cref="ContactSegment"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ContactSegment GetSegment(int id)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          RequestFormat = DataFormat.Json,
                                          Resource = "/assets/contact/segment/" + id
                                      };

            IRestResponse<ContactSegment> response = _client.Execute<ContactSegment>(request);

            return response.Data; 
        }

        /// <summary>
        /// Invoke an HTTP GET request to retrieve a list of <see cref="ContactSegment"/> by search term
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public RequestObjectList<ContactSegment> SearchSegments(string searchTerm, int page, int pageSize)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          RequestFormat = DataFormat.Json,
                                          Resource =
                                              string.Format("/assets/contact/segments?search={0}&page={1}&count={2}",
                                                            searchTerm, page, pageSize)
                                      };

            IRestResponse<RequestObjectList<ContactSegment>> response = _client.Execute<RequestObjectList<ContactSegment>>(request);

            Console.WriteLine("Total: " + response.Data.total);

            return response.Data;
        }

        /// <summary>
        /// Create a new Contact Segment
        /// </summary>
        /// <param name="segment"></param>
        /// <returns>The contact segment as returned from the API</returns>
        public ContactSegment CreateSegment(ContactSegment segment)
        {
            RestRequest request = new RestRequest(Method.POST)
                                      {
                                          RequestFormat = DataFormat.Json,
                                          Resource = "/assets/contact/segment"
                                      };
            request.AddBody(segment);

            IRestResponse<ContactSegment> response = _client.Execute<ContactSegment>(request);
            return response.Data;
        }

        /// <summary>
        /// Update an existnig Contact Segment
        /// </summary>
        /// <param name="contactSegment"></param>
        /// <returns>The contactSegment as returned from the API</returns>
        public ContactSegment UpdateContactSegment(ContactSegment contactSegment)
        {
            RestRequest request = new RestRequest(Method.PUT)
            {
                Resource = "/assets/contact/segment" + contactSegment.id,
                RequestFormat = DataFormat.Json
            };
            request.AddBody(contactSegment);

            IRestResponse<ContactSegment> response = _client.Execute<ContactSegment>(request);

            return response.Data;
        }

        /// <summary>
        /// Delete an <see cref="ContactSegment"/>
        /// </summary>
        /// <param name="id"></param>
        public void DeleteContactSegment(int id)
        {
            RestRequest request = new RestRequest(Method.DELETE) { Resource = "/assets/contact/segment" + id, RequestFormat = DataFormat.Json };

            _client.Execute<ContactSegment>(request);
        }

        #endregion
    }
}
