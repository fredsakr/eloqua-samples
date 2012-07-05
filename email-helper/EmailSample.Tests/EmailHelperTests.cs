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
            _emailHelper = new EmailHelper("site", "user", "pass",
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
            int emailId = 1;
            _emailHelper.DeleteEmail(emailId);
        }

        [Test]
        public void SendEmailTest()
        {
            int contactId = 1;
            Email email = _emailHelper.GetEmail(1);

            _emailHelper.SendEmailToContact(contactId, email);
        }
    }
}
