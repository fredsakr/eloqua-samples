using System.Collections.Generic;
using NUnit.Framework;

namespace ContactImportSample.Tests
{
    [TestFixture]
    public class ContactImportTest
    {
        private ContactImportHelper _contactImportHelper;

        [TestFixtureSetUp]
        public void Init()
        {
            _contactImportHelper = new ContactImportHelper("site", "user", "password",
                                                           "https://secure.eloqua.com/API/Bulk/1.0/");
        }

        [Test]
        public void GetContactFieldsTest()
        {
            var fields = _contactImportHelper.GetFields("*", 1, 1);
            Assert.AreEqual(1, fields.Count);
        }

        [Test]
        public void GetContactListsTest()
        {
            var contactLists = _contactImportHelper.GetContactLists("*", 1, 1);
            Assert.AreEqual(1, contactLists.Count);
        }

        [Test]
        public void CreateImportTest()
        {
            var fields = new Dictionary<string, string> {{"C_EmailAddress", "{{Contact.Field(C_EmailAddress)}}"}};
            var result = _contactImportHelper.CreateImport(fields);
            Assert.IsNotNullOrEmpty(result);
        }

        [Test]
        public void DataImportTest()
        {
            var data = new Dictionary<string, string>();
            data.Add("C_EmailAddress", "test@test.com");
            var result = _contactImportHelper.ImportData("/contact/import/" + 1, data);
            var t = result;
        }

        [Test]
        public void GetSyncResult()
        {
            var syncMessage = _contactImportHelper.CheckSyncResult("/sync/1");
            Assert.Greater(0, syncMessage.elements.Count);
        }
    }
}
