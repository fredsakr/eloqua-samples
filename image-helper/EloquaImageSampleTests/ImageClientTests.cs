using EloquaImageSample;
using NUnit.Framework;

namespace EloquaImageSampleTests
{
    [TestFixture]
    public class ImageClientTests
    {
        private ImageClient _client;

        [TestFixtureSetUp]
        public void Init()
        {
            _client = new ImageClient("site", "user", "password", "https://secure.eloqua.com/API/REST/2.0");
        }

        [Test]
        public void ListFoldersTest()
        {
            var folders = _client.ListFolders();
            Assert.Greater(folders.total, 0);
        }

        [Test]
        public void UploadImageTest()
        {
            var imageName = "sample.jpg";
            var imagePath = "EloquaImageSample.Images.eloqua.jpg";

            var result = _client.UploadImage(imageName, imagePath);

            Assert.AreEqual(result.name, imageName);
        }

        [Test]
        public void DeleteImageTest()
        {
            var imageId = 1;
            var response = _client.DeleteImage(imageId);
            Assert.AreEqual(200, response);
        }
    }
}
