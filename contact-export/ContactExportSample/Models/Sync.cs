using System;

namespace ContactExportSample.Models
{
    public class Sync
    {
        public DateTime? createdAt { get; set; }
        public string createdBy { get; set; }
        public SyncStatusType? status { get; set; }
        public string syncedInstanceUri { get; set; }
        public DateTime? syncEndedAt { get; set; }
        public DateTime? syncStartedAt { get; set; }
        public string uri { get; set; }
    }
}
