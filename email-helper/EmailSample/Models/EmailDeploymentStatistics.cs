namespace EmailSample.Models
{
    public class EmailDeploymentStatistics
    {
        public string bouncebackType { get; set; }
        public int? clickThroughCount { get; set; }
        public string emailAddress { get; set; }
        public long? lastClickThrough { get; set; }
        public long? lastOpen { get; set; }
        public int? openCount { get; set; }
        public long? sentAt { get; set; }
    }
}
