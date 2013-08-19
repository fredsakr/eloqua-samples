using System.Net;
using ContactSample;
using ContactSample.Models;
using NUnit.Framework;

namespace ContactSampleTests
{
    [TestFixture]
    public class ContactClientTests
    {
        #region properties

        private ContactClient _client;

        #endregion

        #region setup / teardown

        [TestFixtureSetUp]
        public void Init()
        {
            _client = new ContactClient("site", "user", "password", "https://secure.eloqua.com/API/REST/2.0");
        }

        #endregion

        #region tests

        [Test]
        public void GetContactTest()
        {
            int id = 1;
            var contact = _client.GetContact(id);
            Assert.AreEqual(contact.id, id);
        }

        [Test]
        public void SearchContactsTest()
        {
            var contacts = _client.SearchContacts("*", 1, 10);
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

            var createdContact = _client.CreateContact(contact);
            Assert.Greater(createdContact.id, 0);
            Assert.AreEqual(contact.emailAddress, createdContact.emailAddress);
        }

        [Test]
        public void DeleteContactTest()
        {
            int id = 1;
            var statusCode = _client.DeleteContact(id);
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
        }

        #endregion

    }
}
