using OptionListSample.Models;
using RestSharp;

namespace OptionListSample
{
    public class OptionListHelper
    {
        private readonly RestClient _client; 

        public OptionListHelper(string site, string user, string password, string baseUrl)
        {
            _client = new RestClient(baseUrl)
                {
                    Authenticator = new HttpBasicAuthenticator(site + "\\" + user, password)
                };
        }

        public OptionList GetList(int id)
        {
            var request = new RestRequest(Method.GET)
                {
                    Resource = string.Format("/assets/optionList/{0}?depth=complete", id)
                };

            var response = _client.Execute<OptionList>(request);

            return response.Data;
        } 

        public SearchResponse<OptionList> SearchOptionLists(string searchTerm, int page, int pageSize)
        {
            var request = new RestRequest(Method.GET)
                {
                    Resource = string.Format("/assets/optionLists?search={0}&page={1}&count={2}&depth=complete", searchTerm, page, pageSize)
                };

            var response = _client.Execute<SearchResponse<OptionList>>(request);
            return response.Data;
        } 

        public OptionList CreateOptionList(OptionList optionList)
        {
            var request = new RestRequest(Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    Resource = string.Format("/assets/optionList")
                };
            request.AddBody(optionList);

            var response = _client.Execute<OptionList>(request);

            return response.Data;
        }

        public ResponseStatus DeleteOptionList(int? optionListId)
        {
            var request = new RestRequest(Method.DELETE)
                {
                    Resource = string.Format("/assets/optionList/{0}", optionListId)
                };

            var response = _client.Execute(request);

            return response.ResponseStatus;
        }
    }
}
