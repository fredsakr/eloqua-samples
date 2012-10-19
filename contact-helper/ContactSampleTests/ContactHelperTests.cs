using System.Net;
using ContactSample;
using ContactSample.Models;
using NUnit.Framework;

namespace ContactSampleTests
{
    [TestFixture]
    public class ContactHelperTests
    {
        #region properties

        private ContactHelper _helper;

        #endregion

        #region setup / teardown

        [TestFixtureSetUp]
        public void Init()
        {
            _helper = new ContactHelper("site", "user", "password", "https://secure.eloqua.com/API/REST/1.0");
        }

        #endregion

        #region tests

        [Test]
        public void GetContactTest()
        {
            int id = 1;
            var contact = _helper.GetContact(id);
            Assert.AreEqual(contact.id, id);
        }

        [Test]
        public void SearchContactsTest()
        {
            var contacts = _helper.SearchContacts("*", 1, 10);
            Assert.Greater(contacts.total, 0);
        }

        [Test]
        public void CreateContactTest()
        {
            Contact contact = new Contact()
                                  {
                                      emailAddress = "sample@test.com",
                                      firstName = "Joe",
                                      lastName = "Smith",
                                      accountName = "Eloqua Corporation",
                                      businessPhone = "123-456-7890"
                                  };

            var createdContact = _helper.CreateContact(contact);
            Assert.Greater(createdContact.id, 0);
            Assert.AreEqual(contact.emailAddress, createdContact.emailAddress);
        }

        [Test]
        public void DeleteContactTest()
        {
            int id = 1;
            var statusCode = _helper.DeleteContact(id);
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
        }

        #endregion

    }
}
