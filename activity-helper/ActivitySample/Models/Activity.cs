using System.Collections.Generic;

namespace ActivitySample.Models
{
    public class Activity
    {
        public int? activityDate { get; set; }
        public ActivityType activityType { get; set; }
        public int? assetId { get; set; }
        public int? contactId { get; set; }
        public Dictionary<string, string> details { get; set; }
        public string id { get; set; }
    }
}
