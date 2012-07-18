using System;
using System.Collections.Generic;
using LandingPageSample.Models;
using RestSharp;

namespace LandingPageSample
{
    public class LandingPageHelper
    {
        #region properties

        private readonly RestClient _client;

        #endregion

        #region constructors

        public LandingPageHelper (string instance, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                          {
                              Authenticator = new HttpBasicAuthenticator(instance + "\\" + user, password)
                          };
        }

        #endregion

        #region LandingPage CRUD Operations

        /// <summary>
        /// Invokes an HTTP GET request to retrieve a <see cref="LandingPage"/> by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public LandingPage GetLandingPage(int id)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          RequestFormat = DataFormat.Json,
                                          Resource = "/assets/landingPage/" + id
                                      };

            IRestResponse<LandingPage> response = _client.Execute<LandingPage>(request);
            return response.Data;
        }

        /// <summary>
        /// Invokes an HTTP GET request to search for a list of <see cref="LandingPage"/>s
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<LandingPage> GetLandingPages(string searchTerm, int page, int pageSize)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          RequestFormat = DataFormat.Json,
                                          Resource =
                                              string.Format("/assets/landingPages?search={0}&page={1}&count={2}",
                                                            searchTerm, page, pageSize)
                                      };

            IRestResponse<RequestObjectList<LandingPage>> response =
                _client.Execute<RequestObjectList<LandingPage>>(request);

            Console.WriteLine("Total : " + response.Data.elements.Count);

            return response.Data.elements;
        }

        /// <summary>
        /// Invokes an HTTP POST to create a new <see cref="LandingPage"/>
        /// </summary>
        /// <param name="landingPage"></param>
        /// <returns></returns>
        public LandingPage CreateLandingPage(LandingPage landingPage)
        {
            RestRequest request = new RestRequest(Method.POST)
                                      {
                                          Resource = "/assets/landingPage",
                                          RequestFormat = DataFormat.Json
                                      };
            request.AddBody(landingPage);

            IRestResponse<LandingPage> response = _client.Execute<LandingPage>(request);

            return response.Data;
        }

        /// <summary>
        /// Update an existnig LandingPage
        /// </summary>
        /// <returns>The LandingPage as returned from the API</returns>
        public LandingPage UpdateLandingPage(LandingPage landingPage)
        {
            RestRequest request = new RestRequest(Method.PUT)
            {
                Resource = "/assets/landingPage/" + landingPage.id,
                RequestFormat = DataFormat.Json
            };
            request.AddBody(landingPage);

            IRestResponse<LandingPage> response = _client.Execute<LandingPage>(request);

            return response.Data;
        }

        /// <summary>
        /// Delete a <see cref="LandingPage"/>
        /// </summary>
        /// <param name="id"></param>
        public void DeleteLandingPage(int id)
        {
            RestRequest request = new RestRequest(Method.DELETE) { Resource = "/assets/landingPage/" + id, RequestFormat = DataFormat.Json };

            _client.Execute<LandingPage>(request);
        }

        #endregion
    }
}