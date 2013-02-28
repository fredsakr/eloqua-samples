namespace ContactSubscriptionSample.Models
{
    public class EmailGroup
    {
        public string depth { get; set; }
        public string description { get; set; }
        public int? id { get; set; }
        public string name { get; set; }
        public string permissions { get; set; }
        public string type
        {
            get { return "EmailGroup"; }
            set {}
        }
        public int? updatedAt { get; set; }
        public int? updatedBy { get; set; }
    }
}
