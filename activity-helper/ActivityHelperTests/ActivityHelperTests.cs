using System;
using ActivitySample;
using NUnit.Framework;

namespace ActivityHelperTests
{
    [TestFixture]
    public class ActivityHelperTests
    {
        private ActivityHelper _activityHelper;

        [TestFixtureSetUp]
        public void Init()
        {
            _activityHelper = new ActivityHelper("site", "user", "password", "https://secure.eloqua.com/API/REST/1.0/");
        }

        [Test]
        public void GetActivities()
        {
            DateTime startDate = new DateTime(2010, 1, 1);
            DateTime endDate = new DateTime(2011, 7, 1);
            var list = _activityHelper.GetActivities(380458, startDate, endDate, Enum.GetName(typeof(ActivitySample.Models.ActivityType), 2), 10);
            Assert.Greater(0, list.activity.Count);
        }
    }
}
