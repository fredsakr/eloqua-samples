using System;
using System.Collections.Generic;

namespace ContactExportSample.Models
{
    public class Export
    {
        public string createdBy { get; set; }
        public DateTime? createdAt { get; set; }
        public Dictionary<string, string> fields { get; set; }
        public ExportFilter filter { get; set; }
        public long? kbUsed { get; set; }
        public string name { get; set; }
        public int? secondsToAutoDelete { get; set; }
        public int? secondsToRetainData { get; set; }
        public List<SyncAction> syncActions { get; set; }
        public string updatedBy { get; set; }
        public DateTime? updatedAt { get; set; }
        public string uri { get; set; }
    }
}
