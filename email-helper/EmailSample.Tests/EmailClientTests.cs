using System.Collections.Generic;
using EmailSample.Models;
using EmailSample.Models.Content;
using NUnit.Framework;

namespace EmailSample.Tests
{
    [TestFixture]
    public class EmailClientTests
    {
        private EmailClient _emailClient;

        [TestFixtureSetUp]
        public void Init()
        {
            _emailClient = new EmailClient("site", "user", "password",
                                                           "https://secure.eloqua.com/API/REST/1.0/");
        }
        [Test]
        public void GetEmailTest()
        {
            int emailId = 1;
            Email email = _emailClient.GetEmail(emailId);
            Assert.AreEqual(1, email.id);
        }

        [Test]
        public void GetEmailListTest()
        {
            List<Email> emails = _emailClient.GetEmail("*", 1, 100, "createdAt", Direction.desc);
            Assert.Greater(0, emails.Count);
        }

        [Test]
        public void CreateEmailTest()
        {
            Email email = new Email()
            {
                name = "sample email",
                emailFooterId = 1,
                emailHeaderId = 1,
                encodingId = 1,
                emailGroupId = 1,
                subject = "sample",
                htmlContent = new RawHtmlContent()
                                  {
                                      type = "RawHtmlContent",
                                      html = "<html><head></head><body>test</body></html>"
                                  }
            };

            Email returnedEmail = _emailClient.CreateEmail(email);
            Assert.AreEqual(email.name, returnedEmail.name);
        }

        [Test]
        public void DeleteEmailTest()
        {
            for (int i = 10; i < 70; i++)
            {
                _emailClient.DeleteEmail(i);
            }

            int emailId = 1;
            _emailClient.DeleteEmail(emailId);
        }

        [Test]
        public void SendEmailToContactTest()
        {
            int contactId = 152365;
            Email email = _emailClient.GetEmail(75);

            _emailClient.SendEmailToContact(contactId, email);
        }

        [Test]
        public void SendCustomEmailToAddress()
        {
            var contact = new Contact()
                              {
                                  id = 1,
                                  emailAddress = "fred.sakr@live.com"
                              };
            var email = new Email()
                            {
                                name = "sample email",
                                htmlContent = new RawHtmlContent()
                                                  {
                                                      html = "<html><head></head><body>test</body></html>",
                                                      type = "RawHtmlContent"
                                                  }
                            };

            var contacts = new List<Contact>();
            contacts.Add(contact);

            var response = _emailClient.SendCustomEmailToAddress(contacts, email);
            Assert.IsNotNull(response);
        }

        [Test]
        public void SearchDeploymentsTest()
        {
            var response = _emailClient.GetDeployments("*", 1, 100);
            Assert.Greater(0, response.Count);
        }

        [Test]
        public void GetDeploymentTest()
        {
            int id = 1;
            var response = _emailClient.GetDeployment(id);
            Assert.AreEqual(id, response.id);
        }
    }
}
