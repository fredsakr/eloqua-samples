using System;

namespace EmailSample.Models
{
    public class Deployment
    {
        public int contactId { get; set; }
        public Email email { get; set; }
        public DateTime endAt { get; set; }
        public int? failedSendCount { get; set; }
        public string name { get; set; }
        public int? sendFromUserId { get; set; }
        public string sentSubject { get; set; }
        public string successfulSendCount { get; set; }
        public string type { get; set; }
    }
}
