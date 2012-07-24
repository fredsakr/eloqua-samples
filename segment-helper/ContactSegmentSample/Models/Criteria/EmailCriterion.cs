using System.Collections.Generic;

namespace ContactSegmentSample.Models.Criteria
{
    public class EmailCriterion : Criterion
    {
        public List<string> emailGroupIds { get; set; } 
        public List<string> emailIds { get; set; }
        public List<string> campaignIds { get; set; } 
    }
}
