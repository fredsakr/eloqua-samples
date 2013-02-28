using ContactSubscriptionSample;
using ContactSubscriptionSample.Models;
using NUnit.Framework;

namespace ContactSubscriptionSampleTests
{
    [TestFixture]
    public class ContactSubscriptionHelperTests
    {
        private ContactSubscriptionHelper _helper;

        [TestFixtureSetUp]
        public void Init()
        {
            _helper = new ContactSubscriptionHelper("site", "user", "password", "https://secure.eloqua.com/API/REST/2.0");
        }

        [Test]
        public void ListContactSubscriptions()
        {
            const int contactId = 1;
            var result = _helper.ListContactSubscriptions(contactId);
            Assert.IsNotNull(result);
        }

        [Test]
        public void UpdateContactSubscription()
        {
            const int contactId = 1;

            var subscription = new Subscription()
                {
                    contactId = contactId,
                    isSubscribed = false,
                    emailGroup = new EmailGroup()
                        {
                            id = 1,
                            name = "My Emails",
                            permissions = "fullControl",
                            depth = "minimal"
                        }
                };

            var result = _helper.UpdateContactSubscription(contactId, subscription);

            Assert.IsNotNull(result);
        }
    }
}
