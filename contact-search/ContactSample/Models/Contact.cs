using System.Collections.Generic;

namespace ContactSample.Models
{
    public class Contact
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string emailAddress { get; set; }
        public int? id { get; set; }
        public List<FieldValue> fieldValues { get; set; }
    }
}
