using System.Collections.Generic;
using NUnit.Framework;
using OptionListSample;
using OptionListSample.Models;

namespace OptionListSampleTests
{
    [TestFixture]
    public class OptionListHelperTests
    {
        private OptionListHelper _helper;

        [TestFixtureSetUp]
        public void Init()
        {
            _helper = new OptionListHelper("site", "user", "password", "https://secure.eloqua.com/API/REST/1.0");
        }

        [Test]
        public void GetOptionListTest()
        {
            const int optionListId = 2;
            var result = _helper.GetList(optionListId);
            Assert.IsNotNull(result);
        }

        [Test]
        public void SearchOptionListsTest()
        {
            var result = _helper.SearchOptionLists("*", 1, 10);
            Assert.Greater(0, result.elements.Count);
        }

        [Test]
        public void CreateOptionListTest()
        {
            var optionList = new OptionList
                {
                    name = "sample list",
                    elements = new List<Option>()
                        {
                            new Option()
                                {
                                    displayName = "sample name",
                                    value = "sample value"
                                }
                        }
                };

            OptionList returnedList = null;
            try
            {
                returnedList = _helper.CreateOptionList(optionList);
                Assert.IsNotNull(returnedList);
            }
            finally
            {
                if (returnedList != null)
                {
                    _helper.DeleteOptionList(returnedList.id);
                }
            }

        }
    }
}
