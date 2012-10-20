using RestSharp;
using UserSample.Models;

namespace UserSample
{
    public class UserHelper
    {
        private readonly RestClient _client;

        public UserHelper(string site, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                          {
                              Authenticator = new HttpBasicAuthenticator(site + "\\" + user, password)
                          };
        }

        public User GetUser(int id)
        {
            var request = new RestRequest(Method.GET)
                              {
                                  Resource = "/system/user/" + id
                              };
            var response = _client.Execute<User>(request);
            return response.Data;
        }

        public SearchResponse<User> SearchUsers (string searchTerm, int page, int pageSize)
        {
            var request = new RestRequest(Method.GET)
                              {
                                  Resource =
                                      string.Format("/system/users?depth=complete&search={0}&page={1}&count={2}",
                                                    searchTerm, page, pageSize)
                              };
            var response = _client.Execute<SearchResponse<User>>(request);
            return response.Data;
        } 
    }
}
