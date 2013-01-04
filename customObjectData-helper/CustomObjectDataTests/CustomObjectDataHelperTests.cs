using CustomObjectDataSample;
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
            _helper = new CustomObjectDataHelper("site", "user", "password", "https://secure.eloqua.com/API/REST/1.0/");
            _fieldHelper = new CustomObjectFieldHelper("site", "user", "password", "https://secure.eloqua.com/API/Bulk/1.0/");
        }
        
        [Test]
        public void GetCustomObjectFieldsTest()
        {
            int customObjectId = 1;
            var result = _fieldHelper.GetCustomObjectFields(customObjectId);
            Assert.IsNotNull(result);
        }

        [Test]
        public void SearchCustomObjectTest()
        {
            int customObjectId = 1;
            var result = _helper.SearchCustomObjects(customObjectId, "Email_Address1=fred.sakr@eloqua.com", 1, 100);
            Assert.Greater(result.elements.Count, 0);
        }
    }
}
