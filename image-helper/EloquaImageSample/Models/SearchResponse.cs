using System.Collections.Generic;

namespace EloquaImageSample.Models
{
    public class SearchResponse<T>
    {
        public List<T> elements { get; set; }
        public int page { get; set; }
        public int pageSize { get; set; }
        public int total { get; set; }
    }
}
