using System;
using System.Collections.Generic;
using CustomObjectSample;
using CustomObjectSample.Models;
using CustomObjectSample.Models.Assets.CustomObjects;
using NUnit.Framework;

namespace CustomObjectSampleTests
{
    [TestFixture]
    public class CustomObjectHelperTests
    {
        #region properties

        private CustomObjectHelper _helper;

        #endregion

        #region setup / teardown

        [TestFixtureSetUp]
        public void Setup()
        {
            _helper = new CustomObjectHelper("site", "user", "password",
                                             "https://secure.eloqua.com/API/REST/1.0/");
        }

        #endregion

        #region tests

        [Test]
        public void GetCustomObjectTest()
        {
            int id = 30; // assumes a customObject exists
            var customObject = _helper.GetCustomObject(id);
            Assert.AreEqual(id, customObject.id);
        }

        [Test]
        public void SearchCustomObjectsTest()
        {
            var list = _helper.SearchCustomObjects("*", 1, 50);

            Assert.Greater(0, list.total);
        }

        [Test]
        public void CreateCustomObjectTest()
        {
            var customObject = new CustomObject
                                   {
                                       id = -10001,
                                       name = "sample",
                                       fields = new List<CustomObjectField>
                                                    {
                                                        new CustomObjectField
                                                            {
                                                                name = "sample text field",
                                                                dataType = Enum.GetName(typeof(DataType), DataType.text),
                                                                type = "CustomObjectField"
                                                            },
                                                        new CustomObjectField
                                                            {
                                                                name = "sample numeric field",
                                                                dataType = Enum.GetName(typeof(DataType), DataType.numeric),
                                                                type = "CustomObjectField"
                                                            },
                                                        new CustomObjectField
                                                            {
                                                                name = "sample date field",
                                                                dataType = Enum.GetName(typeof(DataType), DataType.date),
                                                                type = "CustomObjectField"
                                                            }
                                                    }
                                   };

            var response = _helper.CreateCustomObject(customObject);
            Assert.AreEqual(customObject.name, response.name);
        }

        [Test]
        public void UpdateCustomObjectTest()
        {
            // todo : write test
        }

        [Test]
        public void DeleteCustomObjectTest()
        {
            const int id = 30; // assumes the custom object exists
            var result = _helper.DeleteCustomObject(id);
            Assert.AreEqual(RestSharp.ResponseStatus.Completed, result);
        }

        #endregion
    }
}
