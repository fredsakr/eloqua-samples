using LandingPageSample.Models.Content;

namespace LandingPageSample.Models
{
    public class LandingPage
    {
        public int? createdAt { get; set; }
        public int? createdBy { get; set; }
        public RawHtmlContent htmlContent { get; set; }
        public int? id { get; set; }
        public int? micrositeId { get; set; }
        public string name { get; set; }
        public string style { get; set; }
        public int? updatedAt { get; set; }
        public int? updatedBy { get; set; }
    }
}
