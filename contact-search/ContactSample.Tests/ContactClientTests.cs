using System.Collections.Generic;
using ContactSample.Models;
using NUnit.Framework;

namespace ContactSample.Tests
{
    [TestFixture]
    public class ContactClientTests
    {
        private ContactClient _client;

        [TestFixtureSetUp]
        public void Init()
        {
            _client = new ContactClient("site", "user", "password",
                                                           "https://secure.eloqua.com/API/REST/1.0/");
        }

        [Test]
        public void SearchContactsTest()
        {
            List<Contact> contacts = _client.SearchContacts("*@eloqua.com", 1, 100);
            Assert.Greater(contacts.Count, 0);
        }
    }
}
