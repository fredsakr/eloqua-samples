using System.Collections.Generic;

namespace OptionListSample.Models
{
    public class OptionList
    {
        public int? id { get; set; }
        public string name { get; set; }
        public List<Option> elements { get; set; }  
    }
}
