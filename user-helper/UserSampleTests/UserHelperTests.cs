using NUnit.Framework;
using UserSample;

namespace UserSampleTests
{
    [TestFixture]
    public class UserHelperTests
    {
        private UserHelper _helper;

        [TestFixtureSetUp]
        public void Init()
        {
            _helper = new UserHelper("site", "user", "password", "https://secure.eloqua.com/API/REST/1.0/");
        }

        [Test]
        public void GetUser()
        {
            int id = 10;
            var user = _helper.GetUser(id);
            Assert.AreEqual(user.id, id);
        }

        [Test]
        public void SearchUsersTest()
        {
            var response = _helper.SearchUsers("*", 1, 20);
            Assert.Greater(response.total, 0);
        }
    }
}
