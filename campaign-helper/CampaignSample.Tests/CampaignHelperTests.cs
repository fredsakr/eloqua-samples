using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using RestSharp;
using CampaignSample.Models;

namespace CampaignSample.Tests
{
    [TestFixture]
    public class CampaignHelperTests
    {
        private CampaignHelper _campaignHelper;

        [TestFixtureSetUp]
        public void Init()
        {
            _campaignHelper = new CampaignHelper("site", "user", "password", "https://secure.eloqua.com/API/REST/2.0/");
        }

        [Test]
        public void GetCampaign()
        {
            const int campaignId = 1;
            Campaign campaign = _campaignHelper.SearchCampaigns(campaignId);
            Assert.AreEqual(campaignId, campaign.id);
        }

        [Test]
        public void GetCampaignListTest()
        {
            IRestResponse response = _campaignHelper.SearchCampaigns("*", 1, 100);
            Assert.IsNotNullOrEmpty(response.Content);
        }

        [Test]
        public void CreateAndUpdateCampaign()
        {
            int emailId = 1;
            int segmentId = 1;
            Campaign campaign = _campaignHelper.CreateCampaign(emailId, segmentId);
            Assert.IsNotNull(campaign);

            var originalEmail = campaign.elements[1];

            CampaignEmail email = new CampaignEmail
                {
                    emailId = 2,
                    outputTerminals = originalEmail.outputTerminals,
                    id = originalEmail.id,
                    type = "CampaignEmail"
                };

            campaign.elements[1] = email;

            var updatedCampaign = _campaignHelper.UpdateCampaign(campaign);
            Assert.AreEqual(campaign.id, updatedCampaign.id);
        }

        [Test]
        public void DeleteCampaign()
        {
            int campaignId = 1;
            HttpStatusCode status = _campaignHelper.DeleteCampaign(campaignId);
            Assert.AreEqual(HttpStatusCode.OK, status);
        }

        [Test]
        public void ActivateCampaign()
        {
            int campaignId = 1;
            Campaign campaign = _campaignHelper.ActivateCampaign(campaignId);
            Assert.AreEqual("active", campaign.status);
        }

        [Test]
        public void SearchSegments()
        {
            List<Segment> segments = _campaignHelper.SearchSegments("*", 1, 100);
            Assert.Greater(0, segments.Count);
        }
    }
}