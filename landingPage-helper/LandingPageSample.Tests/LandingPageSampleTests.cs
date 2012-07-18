using System.Collections.Generic;
using NUnit.Framework;
using LandingPageSample.Models;
using LandingPageSample.Models.Content;

namespace LandingPageSample.Tests
{
    [TestFixture]
    public class LandingPageSampleTests
    {
        private LandingPageHelper _landingPageHelper;

        [TestFixtureSetUp]
        public void Init()
        {
            _landingPageHelper = new LandingPageHelper("site", "user", "password", "https://secure.eloqua.com/API/REST/1.0/");
        }

        [Test]
        public void GetLandingPage()
        {
            LandingPage landingPage = _landingPageHelper.GetLandingPage(2);
            Assert.IsNotNull(landingPage);
        }

        [Test]
        public void GetLandingPageListTest()
        {
            List<LandingPage> landingPages = _landingPageHelper.GetLandingPages("*", 1, 10);
            Assert.AreEqual(10, landingPages.Count);
        }

        [Test]
        public void CreateLandingPage()
        {
            LandingPage landingPage = new LandingPage
                                          {
                                              name = "sample landing page",
                                              htmlContent = new RawHtmlContent
                                                                {
                                                                    type = "RawHtmlContent",
                                                                    html =
                                                                        "<html><head></head><body>Sample Landing Page</body></html>"
                                                                }
                                          };
            LandingPage returnedLandingPage = _landingPageHelper.CreateLandingPage(landingPage);
            Assert.AreEqual(landingPage.name, returnedLandingPage.name);
        }

        [Test]
        public void DeleteLandingPageTest()
        {
            int landingPageId = 1;
            _landingPageHelper.DeleteLandingPage(landingPageId);
        }
    }
}
