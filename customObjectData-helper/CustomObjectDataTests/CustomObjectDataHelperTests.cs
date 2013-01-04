using CustomObjectDataSample;
using NUnit.Framework;

namespace CustomObjectDataTests
{
    [TestFixture]
    public class CustomObjectDataHelperTests
    {
        private CustomObjectDataHelper _helper;

        [TestFixtureSetUp]
        public void Init()
        {
            _helper = new CustomObjectDataHelper("site", "user", "password", "https://secure.eloqua.com/API/REST/2.0/");
        }
        
        [Test]
        public void SearchCustomObjectTest()
        {
            int customObjectId = 137;
            var result = _helper.SearchCustomObjects(customObjectId, "Email_Address1=fred.sakr@eloqua.com", 1, 100);
            Assert.Greater(result.elements.Count, 0);
        }
    }
}
