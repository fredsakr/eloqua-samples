using System.Collections.Generic;
using ContactSegmentSample.Models;
using ContactSegmentSample.Models.Conditions;
using ContactSegmentSample.Models.Criteria;
using ContactSegmentSample.Models.Filters;

namespace ContactExportWithFilterSample
{
    public class FilterHelper
    {
        public ContactSegment CreateSegment(List<SegmentElement> filterElements)
        {
            ContactSegment segment = new ContactSegment
            {
                id = -500002, // aribtrary negative number
                elements = filterElements,
                name = "sample segment",
                type = "ContactSegment",
                detph = "complete"
            };
            return segment;
        }

        public ContactFilterSegmentElement CreateFilterWithElements(string filterName, List<Criterion> criteria)
        {
            // Next we'll create a Contact Filter
            ContactFilter filter = new ContactFilter
            {
                criteria = criteria,
                id = -500012,
                name = filterName,
                scope = "local",
                statement = "-500014", // aribtrary negative number
                type = "ContactFilter"
            };

            ContactFilterSegmentElement filterElement = new ContactFilterSegmentElement()
            {
                filter = filter,
                id = -500013, // aribtrary negative number
                isIncluded = true,
                type = "ContactFilterSegmentElement"
            };

            return filterElement;
        }

        public EmailSentCriterion CreateEmailSentCriterion(List<string> emailIds)
        {
            var emailSentCriterion = new EmailSentCriterion
            {
                id = -500014, // aribtrary negative number
                type = "EmailSentCriterion",
                //emailIds = emailIds,
                activityRestriction = CreateActivityRestriction(ConditionOperator.equal),
                timeRestriction = CreateDateValueCondition()
            };

            return emailSentCriterion;
        }

        public NumericValueCondition CreateActivityRestriction(ConditionOperator op)
        {
            return new NumericValueCondition
            {
                id = -500020, // aribtrary negative number
                @operator = op.ToString(),
                type = "NumericValueCondition",
                value = 1
            };
        }

        public DateValueCondition CreateDateValueCondition()
        {
            return new DateValueCondition
            {
                id = -21, 
                @operator = "withinLast",
                type = "DateValueCondition",
                value = new RelativeDate
                {
                    offset = 1,
                    timePeriod = "week",
                    type = "RelativeDate"
                }
            };            
        }

    }
}
