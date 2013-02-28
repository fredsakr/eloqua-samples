namespace ContactSubscriptionSample.Models
{
    public class Subscription
    {
        public int? contactId { get; set; }
        public EmailGroup emailGroup { get; set; }
        public bool? isSubscribed { get; set; }
        public string type
        {
            get { return "ContactEmailSubscription"; }
            set {}
        }
    }
}
