using System.Collections.Generic;

namespace ContactSegmentSample.Models.Filters
{
    public class ContactFilter
    {
        public int? count { get; set; }
        public List<Criteria.Criterion> criteria { get; set; }
        public int? id { get; set; }
        public string lastCalculatedAt { get; set; }
        public string name { get; set; }
        public string scope { get; set; }
        public string statement { get; set; }
        public string type { get; set; }
    }
}
