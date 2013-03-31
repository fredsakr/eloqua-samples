using System;
using ActivitySample;
using NUnit.Framework;

namespace ActivitySampleTests
{
    [TestFixture]
    public class ActivityClientTests
    {
        private ActivityClient _activityClient;

        [TestFixtureSetUp]
        public void Init()
        {
            _activityClient = new ActivityClient("site", "user", "password", "https://secure.eloqua.com/API/REST/1.0/");
        }

        [Test]
        public void GetActivities()
        {
            var startDate = new DateTime(2010, 1, 1);
            var endDate = new DateTime(2011, 7, 1);
            var list = _activityClient.GetActivities(380458, startDate, endDate, ActivitySample.Models.ActivityType.emailSend, 10);
            Assert.Greater(list.Count, 0);
        }
    }
}
