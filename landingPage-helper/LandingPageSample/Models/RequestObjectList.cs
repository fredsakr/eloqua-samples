using System.Collections.Generic;

namespace LandingPageSample.Models
{
    public class RequestObjectList<T>
    {
        public List<T> elements { get; set; }
        public int total { get; set; }
        public int pageSize { get; set; }
        public int page { get; set; }
    }
}