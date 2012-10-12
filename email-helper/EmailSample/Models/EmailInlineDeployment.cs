using System.Collections.Generic;

namespace EmailSample.Models
{
    public class EmailInlineDeployment : EmailDeployment
    {
        public int? clickthroughCount { get; set; }
        public List<Contact> contacts { get; set; }
        public int? openCount { get; set; }
        public int? sendFromUserId { get; set; }
    }
}
