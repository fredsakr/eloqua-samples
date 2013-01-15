using System.Collections.Generic;
using CustomObjectDataSample;
using CustomObjectDataSample.Models;
using CustomObjectDataSample.Models.Data;
using NUnit.Framework;

namespace CustomObjectDataTests
{
    [TestFixture]
    public class CustomObjectDataHelperTests
    {
        private CustomObjectDataHelper _helper;
        private CustomObjectFieldHelper _fieldHelper;

        [TestFixtureSetUp]
        public void Init()
        {
            _helper = new CustomObjectDataHelper("site", "user", "password", "https://secure.eloqua.com/API/REST/2.0/");
            _fieldHelper = new CustomObjectFieldHelper("site", "user", "password", "https://secure.eloqua.com/API/Bulk/2.0/");
        }
        
        [Test]
        public void GetCustomObjectFieldsTest()
        {
            int customObjectId = 160;
            var result = _fieldHelper.GetCustomObjectFields(customObjectId);
            Assert.IsNotNull(result);
        }

        [Test]
        public void SearchCustomObjectTest()
        {
            int customObjectId = 160;
            var result = _helper.SearchCustomObjects(customObjectId, "", 1, 100);
            Assert.Greater(result.elements.Count, 0);
        }

        [Test]
        public void CreateCustomObjectTest()
        {
            int customObjectId = 160;
            var customObjectData = new CustomObjectData()
                {
                    
                    fieldValues = new List<FieldValue>()
                        {
                            new FieldValue()
                                {
                                    id = 896,
                                    value = "fred.sakr@eloqua.com"
                                },
                            new FieldValue()
                                {
                                    id = 898,
                                    value = "unique code"
                                }
                        }
                };

            customObjectData = _helper.CreateCustomObjectData(customObjectId, customObjectData);

            Assert.Greater(0, customObjectData.id);
        }
    }
}
