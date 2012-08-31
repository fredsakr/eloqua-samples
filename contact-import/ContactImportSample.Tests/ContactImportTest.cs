using System.Collections.Generic;
using ContactImportSample.RequestObjects;
using NUnit.Framework;

namespace ContactImportSample.Tests
{
    [TestFixture]
    public class ContactImportTest
    {
        private ContactImportHelper _contactImportHelper;

        private string _site = "site";
        private string _user = "user";
        private string _password = "password";

        [TestFixtureSetUp]
        public void Init()
        {
            _contactImportHelper = new ContactImportHelper(_site, _user, _password,
                                                           "https://secure.eloqua.com/API/Bulk/1.0/");
        }

        #region atomic operation tests

        [Test]
            public void GetContactFieldsTest()
        {
            List<Field> fields = _contactImportHelper.GetFields("*", 1, 1);
            Assert.AreEqual(1, fields.Count);
        }

        [Test]
        public void GetContactListsTest()
        {
            List<ContactList> contactLists = _contactImportHelper.GetContactLists("*", 1, 1);
            Assert.AreEqual(1, contactLists.Count);
        }

        [Test]
        public void CreateImportTest()
        {
            Dictionary<string, string> fields = new Dictionary<string, string>
                             {
                                 {"C_EmailAddress", "{{Contact.Field(C_EmailAddress)}}"},
                                 {"C_FirstName", "{{Contact.Field(C_FirstName)}}"},
                             };

            // the id of the list to which the contacts will be added
            // see ContactImportSample.GetContactList() method for retrieving lists with identifiers
            const int listId = 123;

            var result = _contactImportHelper.CreateImportStructure(fields, listId);
            Assert.IsNotNullOrEmpty(result);
        }

        [Test]
        public void DataImportTest()
        {
            Dictionary<string, string> data = new Dictionary<string, string>
                           {
                               {"C_EmailAddress", "test123@test.com"},
                               {"C_FirstName", "Test123"}
                           };
            Dictionary<string, string> data2 = new Dictionary<string, string>
                           {
                               {"C_EmailAddress", "test456@test.com"},
                               {"C_FirstName", "Test456"}
                           };

            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>
                           {
                               data,
                               data2

                           };
            Sync sync = _contactImportHelper.ImportData("/contact/import/" + 1, list);
            Assert.IsNotNullOrEmpty(sync.uri);
        }

        [Test]
        public void DataImportFromCsvTest()
        {
            string pathToFile = @"c:\tmp\text.csv";
            _contactImportHelper.ImportDataFromCsv("/contact/import/" + 350, pathToFile, _site + "\\" + _user, _password);
        }

        [Test]
        public void GetSyncResult()
        {
            RequestObjectList<SyncResult> syncResult = _contactImportHelper.CheckSyncResult("/sync/554");
            Assert.Greater(0, syncResult.elements.Count);
        }

        #endregion

        #region complete import test

        [Test]
        public void CompleteImportTest()
        {
            // Search for Contact Lists in your install
            // Note : this test uses a hardcoded listId = 123
            // Todo : create list for test and destroy on cleanup
            List<ContactList> contactLists = _contactImportHelper.GetContactLists("*", 1, 1);
            Assert.AreEqual(1, contactLists.Count);

            // Define the list of fields that will be used in the data import
            Dictionary<string, string> fields = new Dictionary<string, string>
                             {
                                 {"C_EmailAddress", "{{Contact.Field(C_EmailAddress)}}"},
                                 {"C_FirstName", "{{Contact.Field(C_FirstName)}}"},
                             };

            // The id of the list to which the contacts will be added
            const int listId = 123;
            
            // Create the definition (structure) of the import
            var importUri = _contactImportHelper.CreateImportStructure(fields, listId);

            // Define contact data that will be imported
            Dictionary<string, string> data = new Dictionary<string, string>
                           {
                               {"C_EmailAddress", "test123@test.com"},
                               {"C_FirstName", "Test123"}
                           };
            Dictionary<string, string> data2 = new Dictionary<string, string>
                           {
                               {"C_EmailAddress", "test456@test.com"},
                               {"C_FirstName", "Test456"}
                           };

            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>
                           {
                               data,
                               data2
                           };

            // Transfer the data to the import Uri
            Sync sync = _contactImportHelper.ImportData("/contact/import/" + importUri, list);

            // Verify the status and result of the synch
            // Note : use polling until the sync is complete
            var result = _contactImportHelper.CheckSyncResult(sync.uri);
        }

        #endregion

    }
}
