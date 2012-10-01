using System.Collections.Generic;
using ContactExportSample;
using ContactExportSample.Models;
using ContactSegmentSample;
using ContactSegmentSample.Models;
using NUnit.Framework;

namespace ContactExportWithFilterSample
{
    [TestFixture]
    public class ContactExportWithFilterTest
    {
        #region properties

        private ContactSegmentHelper _segmentHelper;
        private FilterHelper _filterHelper = new FilterHelper();
        private ExportHelper _exportHelper = new ExportHelper();
        private ContactExportHelper _contactExportHelper;

        #endregion

        #region setup

        [TestFixtureSetUp]
        public void Init()
        {
            var site = "site";
            var user = "user";
            var password = "password";

            _segmentHelper = new ContactSegmentHelper(site, user, password, "https://secure.eloqua.com/API/REST/1.0/");
            _contactExportHelper = new ContactExportHelper(site, user, password, "https://secure.eloqua.com/API/Bulk/1.0/");
        }

        #endregion

        #region contact filters

        [Test]
        public void CreateExportWithFilterTest()
        {
            // Define the list of Emails
            var emailIds = new List<string> {"65"};

            // Define the Segment
            var emailSentCriterion = _filterHelper.CreateEmailSentCriterion(emailIds);

            // The endpoint expects a list, but we'll only include 1 criteria
            List<ContactSegmentSample.Models.Criteria.Criterion> criteria = new List<ContactSegmentSample.Models.Criteria.Criterion>();
            criteria.Add(emailSentCriterion);

            ContactFilterSegmentElement filterElement = _filterHelper.CreateFilterWithElements("sample", criteria);

            // The Segment expects one or more Filters, we'll include 1
            List<SegmentElement> filterElements = new List<SegmentElement>();
            filterElements.Add(filterElement);

            ContactSegment segment = _filterHelper.CreateSegment(filterElements);

            // Create the segment
            var returnedSegment = _segmentHelper.CreateSegment(segment);
            Assert.AreEqual(segment.name, returnedSegment.name);

            // Define Export
            var exportUri = _contactExportHelper.CreateExport(_exportHelper.GetExportFields(), "",
                                                              _exportHelper.CreateExportSegment((int) returnedSegment.id));

            Assert.IsNotNullOrEmpty(exportUri);

            // Create Sync
            Sync sync = _contactExportHelper.CreateSync(exportUri);
            Assert.IsNotNullOrEmpty(sync.uri);

            // Get the sync's status (use polling)
            sync = _contactExportHelper.GetSync(sync.uri);
            Assert.AreEqual("success", sync.status);

            // Retrieve the data
            var result = _contactExportHelper.GetExportData(exportUri);

            Assert.IsNotNull(result);
        }

        #endregion
    }
}