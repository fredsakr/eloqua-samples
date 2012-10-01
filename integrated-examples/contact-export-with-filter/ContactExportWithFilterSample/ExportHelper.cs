using System.Collections.Generic;
using ContactExportSample.Models;

namespace ContactExportWithFilterSample
{
    public class ExportHelper
    {
        public ExportFilter CreateExportSegment(int segmentId)
        {
            return new ExportFilter
            {
                filterRule = FilterRuleType.member,
                membershipUri = "/contact/segment/" + segmentId
            };
        }

        public Dictionary<string, string> GetExportFields()
        {
            return new Dictionary<string, string>
                             {
                                 {"C_EmailAddress", "{{Contact.Field(C_EmailAddress)}}"},
                                 {"C_FirstName", "{{Contact.Field(C_FirstName)}}"},
                             };
        }
    }
}
