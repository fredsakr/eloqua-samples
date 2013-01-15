using System.Collections.Generic;

namespace CustomObjectDataSample.Models.Data
{
    public class CustomObjectData
    {
        public int? id { get; set; }
        public int? contactId { get; set; }
        public string currentStatus { get; set; }
        public List<FieldValue> fieldValues { get; set; }
    }
}
