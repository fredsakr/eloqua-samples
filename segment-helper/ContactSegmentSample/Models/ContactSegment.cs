using System.Collections.Generic;

namespace ContactSegmentSample.Models
{
    public class ContactSegment
    {
        public int? count { get; set; }
        public string detph { get; set; }
        public int? id { get; set; }
        public string name { get; set; }
        public List<SegmentElement> elements { get; set; }
        public string lastCalculatedAt { get; set; }
        public string type { get; set; }
    }
}
