using System;

namespace CustomDataImportSample.Models.CustomObjects
{
    public class CustomObjectSearchResponse
    {
        public DateTime? createdAt { get; set; }
        public string createdBy { get; set; }
        public string displayNameFieldUri { get; set; }
        public string emailAddressFieldUri { get; set; }
        public string name { get; set; }
        public string uniqueFieldUri { get; set; }
        public string uri { get; set; }
    }
}
