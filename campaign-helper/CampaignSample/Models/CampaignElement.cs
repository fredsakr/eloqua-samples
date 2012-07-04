using System.Collections.Generic;

namespace CampaignSample.Models
{
    public class CampaignElement
    {
        public int? id { get; set; }
        public int? contactCount { get; set; }
        public List<CampaignOutputTerminal> outputTerminals { get; set; }
        public Position position { get; set; }
        public string type { get; set; }
    }
}
