using System.Collections.Generic;
using RestSharp;
using NUnit.Framework;
using ContactExportSample.Models;

namespace ContactExportSample.Tests
{
    [TestFixture]
    public class ContactExportHelperTests
    {
        private ContactExportHelper _contactImportHelper;

        [TestFixtureSetUp]
        public void Init()
        {
            _contactImportHelper = new ContactExportHelper("site", "user", "password",
                                                           "https://secure.eloqua.com/API/Bulk/1.0/");
        }

        [Test]
        public void GetContactFieldsTest()
        {
            List<Field> fields = _contactImportHelper.SearchContactFields("*", 1, 1);
            Assert.AreEqual(1, fields.Count);
        }

        [Test]
        public void GetContactFilterTest()
        {
            List<ContactFilter> contactFilters = _contactImportHelper.SearchContactFilters("*", 1, 10);
            Assert.AreEqual(1, contactFilters.Count);
        }

        [Test]
        public void CreateExportTest()
        {
            ExportFilter filter = new ExportFilter
                                      {
                                          filterRule = FilterRuleType.member,
                                          membershipUri = "/contact/filter/100005"
                                      };

            Dictionary<string, string> fields = new Dictionary<string, string>
                             {
                                 {"C_EmailAddress", "{{Contact.Field(C_EmailAddress)}}"},
                                 {"C_FirstName", "{{Contact.Field(C_FirstName)}}"},
                             };

            const string destinationUri = ""; // not used

            string exportUri = _contactImportHelper.CreateExport(fields, destinationUri, filter);

            Assert.IsNotNullOrEmpty(exportUri);
        }

        [Test]
        public void CreateAndCheckExportStatusTest()
        {
            const string exportUri = "/contact/export/147";
            Sync sync = _contactImportHelper.CreateSync(exportUri);
            Assert.IsNotNullOrEmpty(sync.uri);

            // Get the sync's status (wait and try)
            sync = _contactImportHelper.GetSync(sync.uri);
            Assert.AreEqual("active", sync.status);
        }

        [Test]
        public void GetDataTest()
        {
            const string exportUri = "/contact/export/147"; // retrieve from CreateExport)
            IRestResponse result = _contactImportHelper.GetExportData(exportUri);
            Assert.IsNotNull(result);
        }

        [Test]
        public void GetExportsTest()
        {
            List<Export> exports = _contactImportHelper.SearchExports("*", 1, 1);
            Assert.Greater(0, exports.Count);
        }
    }
}