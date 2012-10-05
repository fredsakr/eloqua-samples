using System.Collections.Generic;

namespace CustomObjectSample.Models.Assets.CustomObjects
{
    public class CustomObject
    {
        public string displayNameFieldId { get; set; }
        public List<CustomObjectField> fields { get; set; }
        public int? id { get; set; }
        public string name { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public string searchTerm { get; set; }
        public string uniqueCodeFieldId { get; set; }
    }
}
