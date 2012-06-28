using System;

namespace ContactExportSample.Models
{
    public class ContactFilter
    {
        public int? count { get; set; }
        public string createdBy { get; set; }
        public DateTime? createdAt { get; set; }
        public string name { get; set; }
        public DateTime? executedAt { get; set; }
        public string updatedBy { get; set; }
        public DateTime? updatedAt { get; set; }
        public string uri { get; set; }
    }
}