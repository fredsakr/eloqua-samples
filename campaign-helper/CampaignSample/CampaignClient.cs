using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;
using CampaignSample.Models;

namespace CampaignSample
{
    public class CampaignClient
    {
        #region properties

        private readonly RestClient _client;
        private readonly CampaignElementHelper _elementHelper;

        #endregion

        #region constructors

        public CampaignClient(string instance, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                          {
                              Authenticator = new HttpBasicAuthenticator(instance + "\\" + user, password)
                          };

            _elementHelper = new CampaignElementHelper();
        }

        #endregion

        #region campaign helper

        public Campaign SearchCampaigns(int id)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          Resource = "/assets/campaign/" + id,
                                          RequestFormat = DataFormat.Json
                                      };

            IRestResponse<Campaign> response = _client.Execute<Campaign>(request);
            return response.Data;
        }

        public IRestResponse SearchCampaigns (string searchTerm, int page, int pageSize)
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
        /// Limited to campaigns containing emails and segments
        /// </summary>
        public Campaign CreateCampaign(int emailId, int segmentId)
        {
            CampaignEmail campaignEmail = _elementHelper.GetCampaignEmail(emailId, -101);
            CampaignSegment campaignSegment = _elementHelper.GetCampaignSegment(segmentId, -100, -101);

            Campaign campaign = new Campaign
                                    {
                                        name = "sample campaign",
                                        campaignType = "sample",
                                        type = "Campaign",
                                        startAt = ConvertToUnixEpoch(DateTime.Now),
                                        endAt = ConvertToUnixEpoch(DateTime.Today.AddDays(1)),
                                        elements = new List<CampaignElement>
                                                       {
                                                           campaignSegment,
                                                           campaignEmail
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

        /// <summary>
        /// Limited to campaigns containing emails and segments
        /// </summary>
        public Campaign UpdateCampaign(Campaign campaign)
        {
            RestRequest request = new RestRequest(Method.PUT)
            {
                Resource = string.Format("/assets/campaign/{0}", campaign.id),
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

        #region Unix time

        private static DateTime _unixEpochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static long ConvertToUnixEpoch(DateTime date)
        {
            return (long) new TimeSpan(date.Ticks - _unixEpochTime.Ticks).TotalSeconds;
        }

        #endregion

        #region Segment helpers

        public List<Segment> SearchSegments(string searchTerm, int page, int pageSize)
        {
            RestRequest request = new RestRequest(Method.GET)
                                      {
                                          RequestFormat = DataFormat.Json,
                                          Resource = string.Format("/assets/contact/segments?search={0}&page={1}&count={2}",
                                                            searchTerm, page, pageSize)
                                      };

            IRestResponse<SearchResponse<Segment>> response = _client.Execute<SearchResponse<Segment>>(request);
            List<Segment> segments = response.Data.elements;
            return segments;
        } 

        #endregion
    }
}