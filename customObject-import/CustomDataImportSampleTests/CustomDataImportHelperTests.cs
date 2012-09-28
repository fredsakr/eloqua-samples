using System;
using System.Collections.Generic;
using CustomDataImportSample;
using CustomDataImportSample.Models;
using CustomDataImportSample.Models.CustomObjects;
using NUnit.Framework;

namespace CustomDataImportSampleTests
{
    [TestFixture]
    public class CustomDataImportHelperTests
    {
        #region properties

        private CustomDataImportHelper _helper;
        private CustomDataHelper _dataHelper;

        #endregion

        #region setup

        [TestFixtureSetUp]
        public void Init()
        {
            const string site = "site";
            const string user = "user";
            const string password = "password";

            _helper = new CustomDataImportHelper(site, user, password, "https://secure.eloqua.com/API/bulk/1.0/");
            _dataHelper = new CustomDataHelper(site, user, password, "https://secure.eloqua.com/API/bulk/1.0/");
        }

        #endregion

        #region tests

        [Test]
        public void FullImportTest()
        {
            // search for custom objects
            RestObjectList<CustomObjectSearchResponse> search = _dataHelper.SearchCustomDataObjects("*", 1, 50);
            List<CustomObjectSearchResponse> customObjects = search.elements;

            // retrieve the custom object uri
            string customObjectUri = customObjects[0].uri;

            // assume a known custom object id (use search endpoint for list)
            int customObjectId = 48;

            RestObjectList<Field> customObjectFields = _dataHelper.GetCustomObjectFields(customObjectId);

            foreach (Field customObjectField in customObjectFields.elements)
            {
                // select items to be included in the list of importFields
                Console.WriteLine("internal name: " + customObjectField.internalName);
            }

            // define the list of fields used in the import
            // for the purposes of this sample, the fields have been hardcoded
            Dictionary<string, string> importFields = new Dictionary<string, string>
                                                          {
                                                              {"Email_Address1", "{{CustomObject[48].Field[370]}}"},
                                                              {"First_Name1", "{{CustomObject[48].Field[371]}}"},
                                                              {"Last_Name1", "{{CustomObject[48].Field[372]}}"},
                                                              {
                                                                  "EmailAddressField",
                                                                  "{{CustomObject[48].Contact.Field(C_EmailAddress)}}"
                                                                  }
                                                          };

            // create the structure for the import
            Import import = _helper.CreateImportStructure(customObjectId, importFields);

            string importUri = import.uri;

            // define some sample data
            Dictionary<string, string> data = new Dictionary<string, string>
                                                  {
                                                      {"Email_Address1", "sample"},
                                                      {"First_Name1", "sample"},
                                                      {"Last_Name1", "sample"},
                                                      {"EmailAddressField", "sample@test.com"}
                                                  };

            Dictionary<string, string> data2 = new Dictionary<string, string>
                                                   {
                                                       {"Email_Address1", "sample2"},
                                                       {"First_Name1", "sample2"},
                                                       {"Last_Name1", "sample2"},
                                                       {"EmailAddressField", "sample2@test.com"}
                                                   };

            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>
                                                        {
                                                            data,
                                                            data2
                                                        };

            // import the data
            Sync sync = _helper.ImportData("/contact/import/" + 1, list);
            Assert.IsNotNullOrEmpty(sync.uri);

            // poll results until the sync is complete
            RestObjectList<SyncResult> syncResult = _helper.CheckSyncResult(sync.uri);

            Assert.IsNotNull(syncResult);
        }

        #endregion
    }
}
