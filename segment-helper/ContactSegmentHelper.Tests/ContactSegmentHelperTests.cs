using System.Collections.Generic;
using ContactSegmentSample;
using ContactSegmentSample.Models;
using ContactSegmentSample.Models.Conditions;
using ContactSegmentSample.Models.Criteria;
using ContactSegmentSample.Models.Filters;
using NUnit.Framework;

namespace ContactSegmentHelperTests
{
    [TestFixture]
    public class ContactSegmentHelperTests
    {
        private ContactSegmentHelper _segmentHelper;

        [TestFixtureSetUp]
        public void Init()
        {
            _segmentHelper = new ContactSegmentHelper("site", "user", "password", "https://secure.eloqua.com/API/REST/1.0/");
        }

        [Test]
        public void GetSegmenTest()
        {
            int segmentId = 19053;
            //ContactSegment contactSegment = _segmentHelper.GetSegment(segmentId);
            //Assert.AreEqual(1, contactSegment.id);
        }

        [Test]
        public void SearchSegmentsTest()
        {
            RequestObjectList<ContactSegment> segments = _segmentHelper.SearchSegments("*", 1, 10);
            Assert.AreEqual(10, segments.elements.Count);
        }

        [Test]
        public void CreateSegmentTest()
        {
            // Activity Restriction (number of emails)
            var activityRestriction = new NumericValueCondition
                                          {
                                              id = -500020,
                                              @operator = "equal",
                                              type = "NumericValueCondition",
                                              value = 1
                                          };

            // Time Restriction (sent within the last)
            var timeRestriction = new DateValueCondition
                                      {
                                          id = -21,
                                          @operator = "withinLast",
                                          type = "DateValueCondition",
                                          value = new RelativeDate
                                                      {
                                                          id = "-500021",
                                                          offset = 1,
                                                          timePeriod = "week",
                                                          type = "RelativeDate"
                                                      }
                                      };

            // Define a Criteria for Contacts that received an Email in the last week
            // includes restrictions defined in the previous step
            EmailSentCriterion emailSentCriterion = new EmailSentCriterion
                                                        {
                                                            id = -500014,
                                                            type = "EmailSentCriterion",
                                                            activityRestriction = activityRestriction,
                                                            timeRestriction = timeRestriction
                                                        };

            // The endpoint expects a list, but we'll only include 1 criteria
            List<ContactSegmentSample.Models.Criteria.Criterion> criteria = new List<ContactSegmentSample.Models.Criteria.Criterion>();
            criteria.Add(emailSentCriterion);

            // Next we'll create a Contact Filter
            ContactFilter filter = new ContactFilter
                                       {
                                           criteria = criteria,
                                           id = -500012,
                                           name = "Filter Sample",
                                           scope = "local",
                                           statement = "-500014",
                                           type = "ContactFilter"
                                       };

            ContactFilterSegmentElement filterElement = new ContactFilterSegmentElement()
                                                            {
                                                                filter = filter,
                                                                id = -500013,
                                                                isIncluded = true,
                                                                type = "ContactFilterSegmentElement"
                                                            };

            // The Segment expects one or more Filters, we'll include 1
            List<SegmentElement> filterElements = new List<SegmentElement>();
            filterElements.Add(filterElement);

            ContactSegment segment = new ContactSegment
                                         {
                                             detph = "complete",
                                             id = -500002,
                                             elements = filterElements,
                                             name = "sample segment",
                                             type = "ContactSegment"
                                         };

            var content = _segmentHelper.CreateSegment(segment);

            Assert.IsNotNull(content);
        }
    }
}
