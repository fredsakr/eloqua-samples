using CustomDataImportSample;
using NUnit.Framework;
using RestSharp;

namespace CustomDataImportSampleTests
{
    [TestFixture]
    public class CustomDataTests
    {
        #region properties

        private CustomDataHelper _helper;  

        #endregion

        #region setup

        [TestFixtureSetUp]
        public void Init()
        {
            _helper = new CustomDataHelper("site", "user", "password", "https://secure.eloqua.com/API/bulk/1.0/");
        }

        #endregion

        #region tests

        [Test]
        public void SearchCustomObjectTest()
        {
            var response = _helper.SearchCustomDataObjects("*", 1, 50);
            Assert.Greater(0, response.total);
        }

        [Test]
        public void GetCustomObjectFields()
        {
            // assumes a custom object with id = 48
            var response = _helper.GetCustomObjectFields(48);
            Assert.IsNotNull(response);
        }

        #endregion
    }
}
