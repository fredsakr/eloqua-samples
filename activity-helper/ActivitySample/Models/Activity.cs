using System.Collections.Generic;

namespace ActivitySample.Models
{
    public class Activity
    {
        public int activityDate { get; set; }
        public int asset { get; set; }
        public string activityType { get; set; }
        public string assetType { get; set; }
        public int contact { get; set; }
        public List<Dictionary<string, string>> details { get; set; }
    }
}
