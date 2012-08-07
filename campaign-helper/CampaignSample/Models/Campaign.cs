using System;
using System.Collections.Generic;

namespace CampaignSample.Models
{
    public class Campaign
    {
        public string campaignType { get; set; }
        public string crmId { get; set; }
        public long? endAt { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public List<CampaignElement> elements { get; set; }
        public long? startAt { get; set; }
        public string status { get; set; }
    }
}
