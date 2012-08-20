using System.Collections.Generic;
using RestSharp;
using NUnit.Framework;
using ContactExportSample.Models;

namespace ContactExportSample.Tests
{
    [TestFixture]
    public class ContactExportHelperTests
    {
        private ContactExportHelper _contactExportHelper;

        [TestFixtureSetUp]
        public void Init()
        {
            _contactExportHelper = new ContactExportHelper("site", "user", "password",
                                                           "https://secure.eloqua.com/API/Bulk/1.0/");
        }

        [Test]
        public void GetContactFieldsTest()
        {
            List<Field> fields = _contactExportHelper.SearchContactFields("*", 1, 1);
            Assert.AreEqual(1, fields.Count);
        }

        [Test]
        public void GetContactFilterTest()
        {
            List<ContactFilter> contactFilters = _contactExportHelper.SearchContactFilters("*", 1, 10);
            Assert.AreEqual(1, contactFilters.Count);
        }

        [Test]
        public void CreateExportTest()
        {
            ExportFilter filter = new ExportFilter
                                      {
                                          filterRule = FilterRuleType.member,
                                          membershipUri = "/contact/filter/200007"
                                      };

            Dictionary<string, string> fields = new Dictionary<string, string>
                             {
                                 {"C_EmailAddress", "{{Contact.Field(C_EmailAddress)}}"},
                                 {"C_FirstName", "{{Contact.Field(C_FirstName)}}"},
                             };

            const string destinationUri = ""; // not used

            string exportUri = _contactExportHelper.CreateExport(fields, destinationUri, filter);

            Assert.IsNotNullOrEmpty(exportUri);
        }

        [Test]
        public void CreateAndCheckExportStatusTest()
        {
            const string exportUri = "/contact/export/158";
            Sync sync = _contactExportHelper.CreateSync(exportUri);
            Assert.IsNotNullOrEmpty(sync.uri);

            // Get the sync's status (wait and try)
            sync = _contactExportHelper.GetSync(sync.uri);
            Assert.AreEqual("success", sync.status);
        }

        [Test]
        public void GetDataTest()
        {
            const string exportUri = "/contact/export/1"; // retrieve from CreateExport)
            IRestResponse result = _contactExportHelper.GetExportData(exportUri);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetExportsTest()
        {
            List<Export> exports = _contactExportHelper.SearchExports("*", 1, 1);
            Assert.Greater(0, exports.Count);
        }
    }
}