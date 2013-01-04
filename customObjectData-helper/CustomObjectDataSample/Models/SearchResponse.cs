using System.Collections.Generic;

namespace CustomObjectDataSample.Models
{
    public class SearchResponse<T>
    {
        public List<T> elements { get; set; }
        public int total { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
    }
}
