using System.Collections.Generic;
using EmailSample.Models;
using EmailSample.Models.Content;
using NUnit.Framework;

namespace EmailSample.Tests
{
    [TestFixture]
    public class EmailHelperTests
    {
        private EmailHelper _emailHelper;

        [TestFixtureSetUp]
        public void Init()
        {
            _emailHelper = new EmailHelper("site", "user", "password",
                                                           "https://secure.eloqua.com/API/REST/1.0/");
        }
        [Test]
        public void GetEmailTest()
        {
            int emailId = 1;
            Email email = _emailHelper.GetEmail(emailId);
            Assert.AreEqual(1, email.id);
        }

        [Test]
        public void GetEmailListTest()
        {
            List<Email> emails = _emailHelper.GetEmail("*", 1, 100);
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

            Email returnedEmail = _emailHelper.CreateEmail(email);
            Assert.AreEqual(email.name, returnedEmail.name);
        }

        [Test]
        public void DeleteEmailTest()
        {
            for (int i = 10; i < 70; i++)
            {
                _emailHelper.DeleteEmail(i);
            }

            int emailId = 1;
            _emailHelper.DeleteEmail(emailId);
        }

        [Test]
        public void SendEmailToContactTest()
        {
            int contactId = 152365;
            Email email = _emailHelper.GetEmail(75);

            _emailHelper.SendEmailToContact(contactId, email);
        }

        [Test]
        public void SendCustomEmailToAddress()
        {
            var contact = new Contact()
                              {
                                  id = 152365,
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

            var response = _emailHelper.SendCustomEmailToAddress(contacts, email);
            Assert.IsNotNull(response);
        }

        [Test]
        public void SearchDeploymentsTest()
        {
            var response = _emailHelper.GetDeployments("*", 1, 100);
            Assert.Greater(0, response.Count);
        }

        [Test]
        public void GetDeploymentTest()
        {
            int id = 23;
            var response = _emailHelper.GetDeployment(id);
            Assert.AreEqual(id, response.id);
        }
    }
}
