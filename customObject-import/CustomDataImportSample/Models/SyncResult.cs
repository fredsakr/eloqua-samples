using System;

namespace CustomDataImportSample.Models
{
    public class SyncResult
    {
        public int? count { get; set; }
        public DateTime? createdAt { get; set; }
        public string message { get; set; }
        public string syncUri { get; set; }
    }
}
