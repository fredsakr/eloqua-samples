namespace ContactSegmentSample.Models
{
    public class ContactFilterSegmentElement : SegmentElement
    {
        public string type { get; set; }
        public Filters.ContactFilter filter { get; set; }
    }
}
