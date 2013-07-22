using System.Net;
using EloquaImageSample.Models;
using RestSharp;

namespace EloquaImageSample
{
    public class ImageClient
    {
        private readonly RestClient _client;

        public ImageClient(string site, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                {
                    Authenticator = new HttpBasicAuthenticator(site + "\\" + user, password)
                };
        }

        public SearchResponse<Folder> ListFolders()
        {
            var request = new RestRequest(Method.GET)
                {
                    Resource = string.Format("/assets/image/folder/root/folders")
                };

            var response = _client.Execute<SearchResponse<Folder>>(request);

            return response.Data;
        }

        public ImageFile UploadImage(string imageName, string imagePath)
        {
            var request = new RestRequest(Method.POST)
                {
                    Resource = "/assets/image/content"
                };

            var image = ResourceHelper.GetEmbeddedResource(imagePath);
            request.AddFile(imageName, image, imageName, "image/jpeg");

            var response = _client.Execute<ImageFile>(request);
            return response.Data;
        }

        public HttpStatusCode DeleteImage(int? imageId)
        {
            var request = new RestRequest(Method.DELETE)
                {
                    Resource = string.Format("/asset/image/{0}", imageId)
                };

            var response = _client.Execute(request);

            return response.StatusCode;
        }
    }
}
