using System;

namespace EmailSample.Models
{
    public class EmailDeployment
    {
        public Email email { get; set; }
        public DateTime endAt { get; set; }
        public int? failedSendCount { get; set; }
        public int? id { get; set; }
        public string name { get; set; }
        public string sentSubject { get; set; }
        public string successfulSendCount { get; set; }
        public string type { get; set; }
    }
}
