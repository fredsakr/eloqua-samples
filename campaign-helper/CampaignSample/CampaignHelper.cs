using System.Collections.Generic;
using System.Net;
using RestSharp;
using CampaignSample.Models;

namespace CampaignSample
{
    public class CampaignHelper
    {
        #region properties

        private readonly RestClient _client;

        #endregion

        #region constructors

        public CampaignHelper(string instance, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                          {
                              Authenticator = new HttpBasicAuthenticator(instance + "\\" + user, password)
                          };
        }

        #endregion

        #region campaign helper

        /// <summary>
        /// Invoke an HTTP GET request to retrieve a <see cref="Campaign"/> by ID
        /// Note : Limited to Campaigns containing only Segments and Emails
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Campaign GetCampaign(int id)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          Resource = "/assets/campaign/" + id,
                                          RequestFormat = DataFormat.Json
                                      };

            IRestResponse<Campaign> response = _client.Execute<Campaign>(request);
            return response.Data;
        }

        /// <summary>
        /// Invoke an HTTP GET request to retrieve a List of <see cref="Campaign"/>s
        /// Note : Limited to Campaigns containing only Segments and Emails
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IRestResponse GetCampaign (string searchTerm, int page, int pageSize)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          RequestFormat = DataFormat.Json,
                                          Resource = string.Format("/assets/campaigns?search={0}&page={1}&count={2}&depth=complete",
                                                            searchTerm, page, pageSize)
                                      };

            IRestResponse response = _client.Execute(request);

            return response;
        }

        /// <summary>
        /// Invoke an HTTP POST request to create a new <see cref="Campaign"/>
        /// Note : The campaign will be used to deploy an Email to a Segment
        /// </summary>
        /// <param name="emailId">The ID of the Email that will be sent</param>
        /// <param name="segmentId">The ID of the Segment to which the Email will be sent</param>
        /// <returns></returns>
        public Campaign CreateCampaign(int emailId, int segmentId)
        {
            Campaign campaign = new Campaign
                                    {
                                        name = "sample campaign",
                                        campaignType = "sample",
                                        elements = new List<CampaignElement>
                                                       {
                                                           new CampaignSegment
                                                               {
                                                                   id = -100,
                                                                   type = "CampaignSegment",
                                                                   segmentId = segmentId,
                                                                   isRecurring = false,
                                                                   position = new Position
                                                                                  {
                                                                                      x = 10,
                                                                                      y = 10
                                                                                  },
                                                                   outputTerminals = new List<CampaignOutputTerminal>
                                                                                         {
                                                                                             new CampaignOutputTerminal
                                                                                                 {
                                                                                                     connectedId = -101,
                                                                                                     connectedType = "CampaignEmail",
                                                                                                     terminalType = "out"
                                                                                                 }
                                                                                         }
                                                               },
                                                           new CampaignEmail
                                                               {
                                                                   id = -101,
                                                                   type = "CampaignEmail",
                                                                   emailId = emailId,
                                                                   sendTimePeriod = "sendAllEmailAtOnce",
                                                                   position = new Position
                                                                                  {
                                                                                      x = 100,
                                                                                      y = 100
                                                                                  }
                                                               }
                                                       }
                                    };

            RestRequest request = new RestRequest(Method.POST)
                                      {
                                          Resource = "/assets/campaign",
                                          RequestFormat = DataFormat.Json
                                      };
            request.AddBody(campaign);

            IRestResponse<Campaign> response = _client.Execute<Campaign>(request);

            return response.Data;
        }

        public HttpStatusCode DeleteCampaign(int campaignId)
        {
            RestRequest request = new RestRequest(Method.DELETE)
                                      {
                                          Resource = "/assets/campaign/" + campaignId,
                                          RequestFormat = DataFormat.Json
                                      };

            IRestResponse response =  _client.Execute(request);
            return response.StatusCode;
        }

        #endregion

        #region campaign activation

        public Campaign ActivateCampaign(int campaignId)
        {
            RestRequest request = new RestRequest(Method.POST)
                                      {
                                          Resource = "/assets/campaign/activate/" + campaignId,
                                          RequestFormat = DataFormat.Json
                                      };

            IRestResponse<Campaign> response = _client.Execute<Campaign>(request);

            return response.Data;
        }

        #endregion
    }
}