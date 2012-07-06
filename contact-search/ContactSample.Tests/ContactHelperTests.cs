using System.Collections.Generic;
using ContactSample.Models;
using NUnit.Framework;

namespace ContactSample.Tests
{
    [TestFixture]
    public class ContactHelperTests
    {
        private ContactHelper _contactHelper;

        [TestFixtureSetUp]
        public void Init()
        {
            _contactHelper = new ContactHelper("site", "user", "password",
                                                           "https://secure.eloqua.com/API/REST/1.0/");
        }

        [Test]
        public void SearchContactsTest()
        {
            List<Contact> contacts = _contactHelper.SearchContacts("*@eloqua.com", 1, 100);
            Assert.Greater(contacts.Count, 0);
        }
    }
}
