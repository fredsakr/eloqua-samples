using System.Runtime.Serialization;

namespace ContactSegmentSample.Models
{
    [KnownType(typeof(ContactFilterSegmentElement))]
    public class SegmentElement
    {
        public int? count { get; set; }
        public string name { get; set; }
        public int? id { get; set; }
        public bool? isIncluded { get; set; }
        public string lastCalculatedAt { get; set; }
    }
}
