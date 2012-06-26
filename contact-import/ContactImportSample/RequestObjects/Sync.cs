using System;

namespace ContactImportSample.RequestObjects
{
    public class Sync
    {
        public string syncedInstanceUri { get; set; }
        public DateTime? syncStartedAt { get; set; }
        public DateTime? syncEndedAt { get; set; }
        public SyncStatusType? status { get; set; }
        public DateTime? createdAt { get; set; }
        public string createdBy { get; set; }
        public string uri { get; set; }
    }
}